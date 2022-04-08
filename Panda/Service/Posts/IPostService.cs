using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;

namespace Panda.Services.Posts;

public interface IPostService
{
    Task<PageDto<PostItem>> GetPostList(PostRequest request);

    Task<List<PostItem>> GetLatestPosts(int top);


    Task<PageDto<PostItem>> GetArticleListByCategoryId(PostCategoryRequest request);

    Task<PostDetailItem> AdminGetPost(int id);
    Task<PostDetailItem> GetPost(int id);

    /// <summary>
    ///     �Զ������ӻ�ȡ����
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    Task<PostDetailItem> GetPostByLink(string link);


    Task Delete(int id);

    Task AddOrUpdate(PostAddOrUpdate request);

    Task<PageDto<AdminPostItemResponse>> AdminGetList(AdminPostGetListRequest request);


    Task<PageDto<AdminPostItemResponse>> Search(string keyword);


    Task<PageDto<PostItem>> GetPostListByTagId(int tagId);


    /// <summary>
    ///     ��ȡ��һ����һ��
    /// </summary>
    /// <param name="PostId"></param>
    /// <returns></returns>
    Task<List<PostNextItem>> GetNextPostItem(int PostId);
}