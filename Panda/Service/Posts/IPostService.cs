using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using PostRequest = Panda.Entity.Models.PostRequest;

namespace Panda.Services.Posts;

public interface IPostService
{
    Task<PageDto<PostItem>> GetPostList(PostRequest request);

    Task<List<PostItem>> GetLatestPosts(int top);
    
    
    Task<PageDto<PostItem>> GetArticleListByCategoryId(PostCategoryRequest request);
    
    Task<PostDetailItem> AdminGetPost(int id);
    Task<PostDetailItem> GetPost(int id);

    Task AddOrUpdate(PostAddOrUpdate request);

    Task<PageDto<AdminPostItemResponse>> AdminGetList(AdminPostGetListRequest request);


    Task<PageDto<AdminPostItemResponse>> Search(string keyword);


    Task<PageDto<PostItem>> GetPostListByTagId(int tagId);
}