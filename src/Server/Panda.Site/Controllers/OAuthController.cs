using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Tools.OAuth.Github;

namespace Panda.Site.Controllers;

[AllowAnonymous]
public class OAuthController : Controller
{
    [HttpGet("/oauth/{type}")]
    public async Task<IActionResult> Github(string type,
        [FromServices] GithubOAuth githubOAuth,
        [FromQuery] string code,
        [FromQuery] string state)
    {
        if (type.ToLower() == "github")
        {
            var res = await githubOAuth.AuthorizeCallback(code, state);
            if (res.IsSccess)
            {
                // todo GitHub登录接入
                return Json(res.UserInfo);
                // var id = userInfo.Id;
            }
        }

        return Content("授权未成功！");
    }

    [HttpGet("/oauth/github-login")]
    public IActionResult GithubLogin([FromServices] GithubOAuth githubOAuth)
    {
        return Redirect(githubOAuth.GetAuthorizeUrl());
    }
}