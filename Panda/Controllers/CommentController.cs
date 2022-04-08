using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Responses;
using Panda.Models;
using Panda.Service.Comment;
using Panda.Tools.Email;

namespace Panda.Controllers;

[Route("/api/[controller]/[action]")]
[ApiController]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    private readonly IEmailSender _emailSender;

    public CommentController(ICommentService commentService, IEmailSender emailSender)
    {
        _commentService = commentService;
        _emailSender = emailSender;
    }

    [IgnoreAntiforgeryToken]
    [HttpPost]
    public async Task Submit(CommentRequest commentRequest)
    {
        await _commentService.Submit(commentRequest);
    }

    [IgnoreAntiforgeryToken]
    [HttpGet]
    public string Test()
    {
        _emailSender.SendEmail("984587039@qq.com", "9845", "测试标题", "您的验证码:<span style=\"color:red\">7777</span>");
        return DateTime.Now.ToString();
    }

    [HttpGet]
    public async Task<PageDto<CommentItem>> GetComments([FromQuery] GetCommentRequest request)
    {
        return await _commentService.GetComments(request);
    }
}