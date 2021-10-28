using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using PostRequest = Panda.Entity.Models.PostRequest;

namespace Panda.Services.Posts;

public interface IPostService
{
    Task<PageDto<ArticleItem>> GetPostList(PostRequest request);

    Task<List<ArticleItem>> GetLatestPosts(int top);
    
    
    Task<PageDto<ArticleItem>> GetArticleListByCategoryId(PostCategoryRequest request);
    
    Task<ArticleDetailItem> GetArticle(int id);

    Task AddOrUpdate(PostAddOrUpdate request);

    Task<PageDto<AdminPostItemResponse>> AdminGetList(AdminPostGetListRequest request);


    Task<PageDto<AdminPostItemResponse>> Search(string keyword);
}