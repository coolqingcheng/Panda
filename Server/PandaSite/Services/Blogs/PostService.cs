using Mapster;
using PandaSite.Api.Blogs.Services.Models;
using PandaSite.Data.Blogs;
using PandaSite.Models;
using PandaSite.Models.Blogs;
using PandaSite.Models.Blogs.Dto;
using PandaSite.Repositories.Blogs;
using PandaTools.Exceptions;

namespace PandaSite.Services.Blogs;

public class PostService
{
    private readonly PostVisitRecordRepository _visitRecordRepository;

    private readonly PostRepository _postRepository;

    private readonly ILogger _logger;

    private readonly DbContext _dbContext;

    private readonly TagRepository _tagRepository;

    private readonly PostCatesRepository _postCatesRepository;

    public PostService(PostVisitRecordRepository visitRecordRepository,
        PostRepository postRepository, ILogger<PostService> logger, DbContext dbContext, TagRepository tagRepository,
        PostCatesRepository postCatesRepository)
    {
        _visitRecordRepository = visitRecordRepository;
        _postRepository = postRepository;
        _logger = logger;
        _dbContext = dbContext;
        _tagRepository = tagRepository;
        _postCatesRepository = postCatesRepository;
    }

    public async Task<PostDetailModel> Get(int id)
    {
        var res = await _postRepository.GetById(id);
        return res!;
    }

    public async Task<PageDto<PostItemModel>> GetHomeList(PostRequestModel request)
    {
        return await _postRepository.GetHomeList(request);
    }

    /// <summary>
    /// 编辑文章
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task Edit(PostEditRequest request)
    {
        request.TagStringToList();
        var tran = await _dbContext.Database.BeginTransactionAsync();
        Posts? post;
        var tagList = await _tagRepository.GetTagsAsync(request.Tags);
        var cateList = await _postCatesRepository.GetCatesAsync(request.Cates);
        if (request.Id > 0)
        {
            post = await _postRepository.FindByIdAsync(request.Id);
            request.Adapt(post);
            await _postRepository.UpdateAsync(post!);
        }
        else
        {
            post = request.Adapt<Posts>();
            await _postRepository.AddAsync(post);
        }

        if (post==null)
        {
            await tran.RollbackAsync();
            throw new UserException("文章不存在,更新失败！");
        }
        await _tagRepository.PostSetTagRelationAsync(post, tagList);

        await _postCatesRepository.PostSetCateRelation(post, cateList);

        await tran.CommitAsync();
    }

    /// <summary>
    /// 访问
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="ip"></param>
    /// <param name="userAgent"></param>
    /// <param name="uid"></param>
    /// <returns></returns>
    public async Task Visit(int postId, string ip, string? userAgent, string? uid)
    {
        var post = await _postRepository.FindByIdAsync(postId);
        if (post == null)
        {
            _logger.LogError($"postId:{postId} 未找到");
            return;
        }

        await _visitRecordRepository.VisitAsync(post, ip, userAgent, uid);
    }

    public async Task Top(int Id)
    {
         await _postRepository.Top(Id);
    }
}