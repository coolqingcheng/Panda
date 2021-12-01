using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Entity.UnitOfWork;
using Panda.Repository.Account;
using Panda.Repository.Article;
using Panda.Repository.ArticleCategoryRelation;
using Panda.Repository.Category;
using Panda.Repository.Tags;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;
using Panda.Tools.Web.Html;
using PostRequest = Panda.Entity.Models.PostRequest;

namespace Panda.Services.Posts;

public class PostService : IPostService
{
    private readonly PostRepository _postRepository;

    private readonly CategoryRepository _categoryRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly PostCategoryRelationRepository _postCategoryRelationRepository;

    private readonly TagRelationRepository _tagRelationRepository;

    private readonly PostTagsRepository _postTagsRepository;

    private AccountRepository _accountRepository;

    public PostService(PostRepository postRepository, CategoryRepository categoryRepository,
        IUnitOfWork unitOfWork, PostCategoryRelationRepository postCategoryRelationRepository,
        TagRelationRepository tagRelationRepository, PostTagsRepository postTagsRepository, AccountRepository accountRepository)
    {
        _postRepository = postRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _postCategoryRelationRepository = postCategoryRelationRepository;
        _tagRelationRepository = tagRelationRepository;
        _postTagsRepository = postTagsRepository;
        _accountRepository = accountRepository;
    }

    public async Task<PageDto<PostItem>> GetPostList(PostRequest request)
    {
        return await _postRepository.GetArticleList(request);
    }

    public async Task<List<PostItem>> GetLatestPosts(int top)
    {
        return await _postRepository.GetLatestPosts(top);
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
        var item = await _postRepository.Where(a => a.Id == id && a.Status == PostStatus.Publish)
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
                    TagItems = a.TagsRelations.Select(b => new PostTagItem()
                    {
                        Id = b.Tags.Id,
                        TagName = b.Tags.TagName
                    })
                }).FirstOrDefaultAsync();
        item.IsNullThrow("找不到当前的文章Id");
        var categories = await _categoryRepository.GetCategories(item!.Id);
        item.Categories = categories.Select(a => new PostCategories()
        {
            Id = a.Id,
            CateName = a.categoryName
        }).ToList();
        return item;
    }

    public async Task AddOrUpdate(PostAddOrUpdate request)
    {
        var account = await _accountRepository.GetCurrentAccountsAsync();
        var text = request.Content.GetHtmlText();
        await _unitOfWork.BeginTransactionAsync();
        if (request.Id > 0)
        {
            var post = await _postRepository.Where(a => a.Id == request.Id)
                .Include(a => a.ArticleCategoryRelations).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new UserException("修改的文章不存在");
            }

            post.Content = request.Content.LazyHandler(request.Title)!;
            post.Text = text;
            post.Summary = text.GetSummary(80);
            post.UpdateTime = DateTime.Now;
            post.Cover = request.Cover;
            post.Account = account;
            await _postRepository.SaveAsync();
            var beforeCategories = await _postCategoryRelationRepository.Where(a => a.Posts == post)
                .Select(a => a.Categories.Id).ToListAsync();
            var afterCategories = await _categoryRepository.Where(a => request.Categories.Contains(a.Id))
                .Select(a => a.Id).ToListAsync();
            await _postCategoryRelationRepository.DiffUpdateRelation(post, beforeCategories, afterCategories);

            await _tagRelationRepository.PostDeleteRelation(post);
            if (request.Tags != null)
            {
                foreach (var tag in request.Tags)
                {
                    var tagItem = await _postTagsRepository.GetWithCreate(tag);
                    await _tagRelationRepository.AddRelationAsync(post, tagItem);
                }
            }
        }
        else
        {
            //添加

            var post = new Entity.DataModels.Posts()
            {
                Title = request.Title,
                Content = request.Content.LazyHandler(request.Title)!,
                AddTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Text = text,
                Summary = text.GetSummary(80),
                Cover = request.Cover,
                Account = account
            };
            await _postRepository.AddAsync(post);
            var categories = await _categoryRepository.Where(a => request.Categories.Contains(a.Id)).ToListAsync();
            foreach (var category in categories)
            {
                category.Count += 1;
                await _postCategoryRelationRepository.AddRelationAsync(post, category);
            }

            if (request.Tags != null)
            {
                foreach (var tagName in request.Tags)
                {
                    var tag = await _postTagsRepository.GetWithCreate(tagName);
                    await _tagRelationRepository.AddRelationAsync(post, tag);
                }
            }

            await _postRepository.SaveAsync();
        }

        await _unitOfWork.CommitAsync();
    }

    public async Task<PageDto<AdminPostItemResponse>> AdminGetList(AdminPostGetListRequest request)
    {
        var res = await _postRepository.AdminGetList(request);
        return res;
    }

    public async Task<PageDto<AdminPostItemResponse>> Search(string keyword)
    {
        return await _postRepository.SearchPost(keyword);
    }

    public async Task<PageDto<PostItem>> GetPostListByTagId(int tagId)
    {
        var query = _tagRelationRepository.Where(a => a.Tags.Id == tagId);
        var tagItem = await query
            .Include(a => a.Posts)
            .Select(a => new PostItem()
            {
                Id = a.Posts.Id,
                Account = a.Posts.Account,
                AddTime = a.AddTime,
                Summary = a.Posts.Summary,
                Title = a.Posts.Title,
                Categories =
                    a.Posts.ArticleCategoryRelations.Select(b => new PostCategories()
                    { Id = b.Categories.Id, CateName = b.Categories.categoryName })
                        .ToList()
            }).OrderByDescending(a => a.AddTime).ToListAsync();

        return new PageDto<PostItem>()
        {
            Data = tagItem,
            Total = await query.CountAsync()
        };
    }
}