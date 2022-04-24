using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Panda.App.Services.Post.Models;
using Panda.Entity.DataModels;
using Panda.Entity.Options;
using Panda.Tools.Extensions;
using Panda.Tools.Web.RSS;
using TencentCloud.Ssl.V20191205.Models;

namespace Panda.App.Services.Post;

public class PostService : IPostService
{
    private readonly DbContext _dbContext;

    private readonly IConfiguration _configuration;

    private readonly SiteOption _siteOption;

    public PostService(DbContext dbContext, IConfiguration configuration, IOptions<SiteOption> siteOption)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _siteOption = siteOption.Value;
    }

    public Task<List<NewestPostModel>> GetNewest(int top)
    {
        return _dbContext.Set<Posts>().Where(a => a.Status == PostStatus.Publish)
            .OrderByDescending(a => a.UpdateTime).Take(top).Select(a => new NewestPostModel()
            {
                Title = a.Title,
                UpdateTime = a.UpdateTime,
                Link = a.CustomLink
            }).ToListAsync();
    }

    public Task<List<string>> GetTags(int pageIndex, int pageSize)
    {
        return _dbContext.Set<PostTags>().OrderByDescending(a => a.AddTime).Page(pageIndex, pageSize)
            .Select(a => a.TagName).ToListAsync();
    }

    public Task<List<CategoriesModel>> GetCategories(int top)
    {
        return _dbContext.Set<Categorys>().Where(a => a.IsShow).OrderByDescending(a => a.AddTime).Take(top).Select(a =>
            new CategoriesModel()
            {
                Id = a.Id,
                CateName = a.CategoryName,
                Count = a.Count,
            }).ToListAsync();
    }

    public async Task<RssModel> GetRssData(int top)
    {
        var list = await _dbContext.Set<Posts>().Where(a => a.Status == PostStatus.Publish)
            .OrderByDescending(a => a.AddTime).Take(top)
            .Select(a => new RssItem()
            {
                Title = a.Title,
                Description = a.Summary,
                Link = $"{_siteOption.Domain}/post/{a.CustomLink}.html"
            }).ToListAsync();
        return new RssModel()
        {
            Title = _siteOption.Name, Link = _siteOption.Domain,
            Item = list
        };
    }
}