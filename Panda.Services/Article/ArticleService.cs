using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Entity.UnitOfWork;
using Panda.Repository.Article;
using Panda.Repository.ArticleCategoryRelation;
using Panda.Repository.Category;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;
using ArticleRequest = Panda.Entity.Models.ArticleRequest;

namespace Panda.Services.Article;

public class ArticleService : IArticleService
{
    private readonly ArticleRepository _articleRepository;

    private readonly CategoryRepository _categoryRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly ArticleCategoryRelationRepository _articleCategoryRelationRepository;

    public ArticleService(ArticleRepository articleRepository, CategoryRepository categoryRepository,
        IUnitOfWork unitOfWork, ArticleCategoryRelationRepository articleCategoryRelationRepository)
    {
        _articleRepository = articleRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _articleCategoryRelationRepository = articleCategoryRelationRepository;
    }

    public async Task<PageResponse<ArticleItem>> GetArticleList(ArticleRequest request)
    {
        return await _articleRepository.GetArticleList(request.Index, request.Size);
    }

    public async Task<PageResponse<ArticleItem>> GetArticleListByCategoryId(ArticleCategoryRequest request)
    {
        return await Task.FromResult(new PageResponse<ArticleItem>());
    }

    public async Task<ArticleDetailItem> GetArticle(int id)
    {
        var item = await _articleRepository.Where(a => a.Id == id).Include(a => a.Account).Select(a =>
            new ArticleDetailItem
            {
                Id = a.Id,
                Title = a.Title,
                Summary = a.Summary,
                Content = a.Content,
                AddTime = a.AddTime,
                AccountId = a.Account.Id,
                AccountName = a.Account.UserName
            }).FirstOrDefaultAsync();
        var categories = await _categoryRepository.GetCategories(item.Id);
        item.Categories = categories.Select(a => new ArticleCategories()
        {
            Id = a.Id,
            CateName = a.categoryName
        }).ToList();
        return item;
    }

    public async Task AddOrUpdate(ArticleAddOrUpdate request)
    {
        var text = request.Content.GetHtmlText();
        await _unitOfWork.BeginTransactionAsync();
        if (request.Id > 0)
        {
            var article = await _articleRepository.Where(a => a.Id == request.Id)
                .Include(a => a.ArticleCategoryRelations).FirstOrDefaultAsync();
            if (article == null)
            {
                throw new UserException("修改的文章不存在");
            }
            article.Content = request.Content;
            article.Text = text;
            article.Summary = text.GetSummary(80);
            article.UpdateTime = DateTime.Now;
            await _articleRepository.SaveAsync();
            var beforeCategories = await _articleCategoryRelationRepository.Where(a=>a.Articles==article).Select(a => a.Categories.Id).ToListAsync();
            var afterCategories = await _categoryRepository.Where(a => request.Categories.Contains(a.Id)).Select(a=>a.Id).ToListAsync();
            await _articleCategoryRelationRepository.DiffUpdateRelation(article, beforeCategories, afterCategories);
        }
        else
        {
            //添加

            var entity = new Articles()
            {
                Title = request.Title,
                Content = request.Content,
                AddTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Text = text,
                Summary = text.GetSummary(80)
            };
            await _articleRepository.AddAsync(entity);
            var categories = await _categoryRepository.Where(a => request.Categories.Contains(a.Id)).ToListAsync();
            foreach (var category in categories)
            {
                await _articleCategoryRelationRepository.AddRelationAsync(entity, category);
            }

            await _articleRepository.SaveAsync();
        }

        await _unitOfWork.CommitAsync();
    }

    public async Task<PageResponse<AdminArticleItemResponse>> AdminGetList(AdminArticleGetListRequest request)
    {
        var res = await _articleRepository.AdminGetList(request);
        return res;

    }
}