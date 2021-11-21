using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Entity.UnitOfWork;
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

    public PostService(PostRepository postRepository, CategoryRepository categoryRepository,
        IUnitOfWork unitOfWork, PostCategoryRelationRepository postCategoryRelationRepository,
        TagRelationRepository tagRelationRepository, PostTagsRepository postTagsRepository)
    {
        _postRepository = postRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _postCategoryRelationRepository = postCategoryRelationRepository;
        _tagRelationRepository = tagRelationRepository;
        _postTagsRepository = postTagsRepository;
    }

    public async Task<PageDto<ArticleItem>> GetPostList(PostRequest request)
    {
        return await _postRepository.GetArticleList(request);
    }

    public async Task<List<ArticleItem>> GetLatestPosts(int top)
    {
        return await _postRepository.GetLatestPosts(top);
    }

    public async Task<PageDto<ArticleItem>> GetArticleListByCategoryId(PostCategoryRequest request)
    {
        return await Task.FromResult(new PageDto<ArticleItem>());
    }

    public async Task<ArticleDetailItem> AdminGetArticle(int id)
    {
        var item = await GetArticle(id);
        item.Content = item.Content.RestoreLazyHandler()!;
        return item;
    }

    public async Task<ArticleDetailItem> GetArticle(int id)
    {
        var item = await _postRepository.Where(a => a.Id == id && a.Status == PostStatus.Publish)
            .Include(a => a.Account).Select(a =>
                new ArticleDetailItem
                {
                    Id = a.Id,
                    Title = a.Title,
                    Summary = a.Summary,
                    Content = a.Content,
                    AddTime = a.AddTime,
                    AccountId = a.Account.Id,
                    AccountName = a.Account.UserName,
                    Status = a.Status
                }).FirstOrDefaultAsync();
        var categories = await _categoryRepository.GetCategories(item.Id);
        item.Categories = categories.Select(a => new ArticleCategories()
        {
            Id = a.Id,
            CateName = a.categoryName
        }).ToList();
        return item;
    }

    public async Task AddOrUpdate(PostAddOrUpdate request)
    {
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
            await _postRepository.SaveAsync();
            var beforeCategories = await _postCategoryRelationRepository.Where(a => a.Posts == post)
                .Select(a => a.Categories.Id).ToListAsync();
            var afterCategories = await _categoryRepository.Where(a => request.Categories.Contains(a.Id))
                .Select(a => a.Id).ToListAsync();
            await _postCategoryRelationRepository.DiffUpdateRelation(post, beforeCategories, afterCategories);

            await _tagRelationRepository.PostDeleteRelation(post);
            foreach (var tag in request.Tags)
            {
                var tagItem = await _postTagsRepository.GetWithCreate(tag);
                await _tagRelationRepository.AddRelationAsync(post, tagItem);
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
                Cover = request.Cover
            };
            await _postRepository.AddAsync(post);
            var categories = await _categoryRepository.Where(a => request.Categories.Contains(a.Id)).ToListAsync();
            foreach (var category in categories)
            {
                category.Count += 1;
                await _postCategoryRelationRepository.AddRelationAsync(post, category);
            }

            foreach (var tagName in request.Tags)
            {
                var tag = await _postTagsRepository.GetWithCreate(tagName);
                await _tagRelationRepository.AddRelationAsync(post, tag);
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
}