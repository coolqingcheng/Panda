using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Responses;
using Panda.Models;
using Panda.Service.Comment;


namespace Panda.Controllers;


[Route("/api/[controller]/[action]")]
[ApiController]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [IgnoreAntiforgeryToken]
    [HttpPost()]
    public async Task Submit(CommentRequest commentRequest)
    {
        await _commentService.Submit(commentRequest);
    }

    [IgnoreAntiforgeryToken]
    [HttpPost()]
    public string Test()
    {
        return DateTime.Now.ToString();
    }

    [HttpGet]
    public async Task<PageDto<CommentItem>> GetComments([FromQuery] GetCommentRequest request)
    {
        return await _commentService.GetComments(request);
    }
}