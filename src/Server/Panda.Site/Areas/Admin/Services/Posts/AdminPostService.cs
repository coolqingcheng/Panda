using Markdig;
using Microsoft.EntityFrameworkCore;
using Panda.Admin.Repositorys;
using Panda.Entity.DataModels;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Tools.Auth.Models;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;
using Panda.Tools.Web.Html;

namespace Panda.Site.Areas.Admin.Services.Posts;

public class PostService : IPostService
{
    private readonly DbContext _dbContext;

    private readonly AccountRepository _accountRepository;

    public PostService(DbContext dbContext, AccountRepository accountRepository)
    {
        _dbContext = dbContext;
        _accountRepository = accountRepository;
    }

    public async Task<PageDto<PostItem>> GetPostList(PostRequest request)
    {
        var query = _dbContext.Set<Entity.DataModels.Posts>().AsQueryable();

        var list = await query.Page(request).Select(a => new PostItem()
        {
            Id = a.Id,
            Title = a.Title,
            Summary = a.Summary,
            Cover = a.Cover,
            CustomLink = a.CustomLink,
            AddTime = a.AddTime,
            Account = a.Account,
            Categories = a.ArticleCategoryRelations.Select(b => new PostCategories()
            {
                CateName = b.Categories.CategoryName,
                Id = b.Categories.Id
            }).ToList(),
            MarkDown = a.MarkDown,
            Status = a.Status
        }).ToListAsync();
        return new PageDto<PostItem>()
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }

    public async Task<PageDto<PostItem>> GetArticleListByCategoryId(PostCategoryRequest request)
    {
        return await Task.FromResult(new PageDto<PostItem>());
    }

    public async Task<PostDetailItem> AdminGetPost(int id)
    {
        var item = await GetPost(id);
        item.Content = item.Content.RestoreLazyHandler()!;
        return item;
    }

    public async Task<PostDetailItem> GetPost(int id)
    {
        var item = await _dbContext.Set<Entity.DataModels.Posts>()
            .Where(a => a.Id == id)
            .Include(a => a.Account).Include(a => a.TagsRelations).Select(a =>
                new PostDetailItem
                {
                    Id = a.Id,
                    Title = a.Title,
                    Summary = a.Summary,
                    Content = a.Content,
                    AddTime = a.AddTime,
                    Account = a.Account,
                    Status = a.Status,
                    Cover = a.Cover,
                    MarkDown = a.MarkDown,
                    CustomLink = a.CustomLink!,

                    TagItems = a.TagsRelations.Select(b => new PostTagItem
                    {
                        Id = b.Tags.Id,
                        TagName = b.Tags.TagName
                    }),
                    Categories = a.ArticleCategoryRelations.Where(c => c.Posts == a).Select(c => new PostCategories()
                    {
                        Id = c.Categories.Id,
                        CateName = c.Categories.CategoryName
                    }).ToList()
                }).FirstOrDefaultAsync();
        item.IsNullThrow("找不到当前的文章Id");
        return item;
    }

    public async Task Delete(int id)
    {
        var tran = await _dbContext.Database.BeginTransactionAsync();
        var postItem = await _dbContext.Set<Entity.DataModels.Posts>().Where(a => a.Id == id).FirstOrDefaultAsync();
        if (postItem != null) _dbContext.Set<Entity.DataModels.Posts>().Remove(postItem);
        await _dbContext.SaveChangesAsync();
        await tran.CommitAsync();
    }

    public async Task AddOrUpdate(PostAddOrUpdate request)
    {
        var account = await _accountRepository.GetCurrentAccountsAsync<Accounts>();
        string htmlContent = Markdown.ToHtml(request.MarkDown);
        var text = htmlContent.GetHtmlText();
        var tran = await _dbContext.Database.BeginTransactionAsync();
        if (request.Id > 0)
        {
            var post = await _dbContext.Set<Entity.DataModels.Posts>().Where(a => a.Id == request.Id)
                .Include(a => a.ArticleCategoryRelations).FirstOrDefaultAsync();
            if (post == null) throw new UserException("修改的文章不存在");

            post.Title = request.Title;
            post.Content = htmlContent.LazyHandler(request.Title)!;
            post.Text = text;
            post.Summary = text.GetSummary(80);
            post.UpdateTime = DateTime.Now;
            post.Cover = request.Cover;
            post.Account = account;
            post.MarkDown = request.MarkDown;
            post.Status = request.Status;
            Console.WriteLine("发布状态：" + request.Status);
            if (string.IsNullOrWhiteSpace(post.CustomLink)) post.CustomLink = Guid.NewGuid().ToString("N");

            await _dbContext.SaveChangesAsync();

            var oldCategories = await _dbContext.Set<PostsCategoryRelations>().Where(a => a.Posts == post)
                .ToListAsync();
            var newCategories = await _dbContext.Set<Categorys>().Where(a => request.Categories.Contains(a.Id))
                .ToListAsync();
            var newRelations = new List<PostsCategoryRelations>();
            foreach (var category in newCategories)
            {
                newRelations.Add(new PostsCategoryRelations()
                {
                    Posts = post,
                    Categories = category
                });
            }

            _dbContext.RemoveRange(oldCategories);
            await _dbContext.SaveChangesAsync();
            await _dbContext.AddRangeAsync(newRelations);
            await _dbContext.SaveChangesAsync();
            //删除标签关系
            var tagRelationList = await _dbContext.Set<TagsRelation>().Where(a => a.Posts == post).ToListAsync();
            _dbContext.RemoveRange(tagRelationList);
            await _dbContext.SaveChangesAsync();
            //重新添加标签关系
            if (request.Tags != null)
                foreach (var tag in request.Tags)
                {
                    var tagItem = await _dbContext.Set<PostTags>().Where(a => a.TagName == tag).FirstOrDefaultAsync();
                    if (tagItem == null)
                    {
                        tagItem = new PostTags()
                        {
                            TagName = tag
                        };
                        await _dbContext.Set<PostTags>().AddAsync(tagItem);
                        await _dbContext.SaveChangesAsync();
                    }

                    await _dbContext.Set<TagsRelation>().AddAsync(new TagsRelation()
                    {
                        Tags = tagItem,
                        Posts = post
                    });
                }

            await _dbContext.SaveChangesAsync();
        }
        else
        {
            //添加

            var post = new Entity.DataModels.Posts
            {
                Title = request.Title,
                Content = htmlContent.LazyHandler(request.Title)!,
                AddTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Text = text,
                Summary = text.GetSummary(80),
                Cover = request.Cover,
                Account = account,
                MarkDown = request.MarkDown,
                CustomLink = Guid.NewGuid().ToString("N"),
                Status = request.Status
            };
            await _dbContext.Set<Entity.DataModels.Posts>().AddAsync(post);
            await _dbContext.SaveChangesAsync();
            var categories = await _dbContext.Set<Categorys>().Where(a => request.Categories.Contains(a.Id))
                .ToListAsync();
            foreach (var category in categories)
                await _dbContext.Set<PostsCategoryRelations>().AddAsync(new PostsCategoryRelations()
                {
                    Posts = post,
                    Categories = category
                });

            if (request.Tags != null)
                foreach (var tag in request.Tags)
                {
                    var tagItem = await _dbContext.Set<PostTags>().Where(a => a.TagName == tag).FirstOrDefaultAsync();
                    if (tagItem == null)
                    {
                        tagItem = new PostTags()
                        {
                            TagName = tag
                        };
                        await _dbContext.Set<PostTags>().AddAsync(tagItem);
                        await _dbContext.SaveChangesAsync();
                    }

                    await _dbContext.Set<TagsRelation>().AddAsync(new TagsRelation()
                    {
                        Tags = tagItem,
                        Posts = post
                    });
                }

            await _dbContext.SaveChangesAsync();
        }

        await tran.CommitAsync();
    }

    public async Task<PageDto<AdminPostItemResponse>> AdminGetList(AdminPostGetListRequest request)
    {
        var query = _dbContext.Set<Entity.DataModels.Posts>()
            .WhereIf(request.KeyWord != null, a => a.Title.Contains(request.KeyWord!))
            .AsQueryable();
        var list = await query.OrderByDescending(a => a.Status).ThenByDescending(a => a.AddTime).Page(request).Select(
            a => new AdminPostItemResponse()
            {
                Id = a.Id,
                Title = a.Title,
                Summary = a.Summary,
                AccountName = a.Account.NickName,
                AddTime = a.AddTime,
                CategoryItems = a.ArticleCategoryRelations.Select(b => new AdminCategoryItem()
                {
                    CateName = b.Categories.CategoryName,
                    Id = b.Categories.Id
                }).ToList(),
                CustomLink = a.CustomLink,
                Status = a.Status,
                UpdateTime = a.UpdateTime
            }).ToListAsync();
        return new PageDto<AdminPostItemResponse>()
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }

    public async Task<PageDto<AdminPostItemResponse>> Search(string keyword)
    {
        // return await _postRepository.SearchPost(keyword);
        return null;
    }

    public async Task<PageDto<PostItem>> GetPostListByTagId(int tagId)
    {
        var query = _dbContext.Set<TagsRelation>().Where(a => a.Tags.Id == tagId);
        var tagItem = await query
            .Include(a => a.Posts)
            .Select(a => new PostItem
            {
                Id = a.Posts.Id,
                Account = a.Posts.Account,
                AddTime = a.AddTime,
                Summary = a.Posts.Summary,
                Title = a.Posts.Title,
                CustomLink = a.Posts.CustomLink!,
                Categories =
                    a.Posts.ArticleCategoryRelations.Select(b => new PostCategories
                            { Id = b.Categories.Id, CateName = b.Categories.CategoryName })
                        .ToList()
            }).OrderByDescending(a => a.AddTime).ToListAsync();

        return new PageDto<PostItem>
        {
            Data = tagItem,
            Total = await query.CountAsync()
        };
    }

    public async Task<List<PostNextItem>> GetNextPostItem(int PostId)
    {
        var list = new List<PostNextItem>();
        var nextPost = await _dbContext.Set<Entity.DataModels.Posts>().Where(a => a.Id == PostId)
            .OrderBy(a => a.AddTime).Skip(1).Take(1).Select(
                a =>
                    new PostNextItem
                    {
                        Id = a.Id,
                        Title = a.Title,
                        CustomLink = a.CustomLink!,
                        Type = PostNextType.Next
                    }).FirstOrDefaultAsync();
        var prePost = await _dbContext.Set<Entity.DataModels.Posts>().Where(a => a.Id == PostId)
            .OrderByDescending(a => a.AddTime).Skip(1).Take(1)
            .Select(
                a => new PostNextItem
                {
                    Id = a.Id,
                    Title = a.Title,
                    CustomLink = a.CustomLink!,
                    Type = PostNextType.Pre
                }).FirstOrDefaultAsync();
        if (nextPost != null) list.Add(nextPost);

        if (prePost != null) list.Add(prePost);

        return list;
    }

    public async Task<PostDetailItem> GetPostByLink(string link)
    {
        var item = await _dbContext.Set<Entity.DataModels.Posts>()
            .Where(a => a.CustomLink == link && a.Status == PostStatus.Publish)
            .Include(a => a.Account).Include(a => a.TagsRelations).Select(a =>
                new PostDetailItem
                {
                    Id = a.Id,
                    Title = a.Title,
                    Summary = a.Summary,
                    Content = a.Content,
                    AddTime = a.AddTime,
                    Account = a.Account,
                    Status = a.Status,
                    Cover = a.Cover,
                    MarkDown = a.MarkDown,
                    CustomLink = a.CustomLink!,
                    TagItems = a.TagsRelations.Select(b => new PostTagItem
                    {
                        Id = b.Tags.Id,
                        TagName = b.Tags.TagName
                    })
                }).FirstOrDefaultAsync();
        item.IsNullThrow("找不到当前的文章Id");
        item.Categories = await _dbContext.Set<Categorys>().Select(a => new PostCategories
        {
            Id = a.Id,
            CateName = a.CategoryName
        }).ToListAsync();
        return item;
    }
}