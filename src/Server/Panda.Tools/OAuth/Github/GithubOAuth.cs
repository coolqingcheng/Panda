using MrHuo.OAuth;

namespace Panda.Tools.OAuth.Github;

public class GithubOAuth:OAuthLoginBase<GithubUserModel>
{
    public GithubOAuth(OAuthConfig oauthConfig) : base(oauthConfig) { }
    protected override string AuthorizeUrl => "https://github.com/login/oauth/authorize";
    protected override string AccessTokenUrl => "https://github.com/login/oauth/access_token";
    protected override string UserInfoUrl => "https://api.github.com/user";
    public override async Task<GithubUserModel> GetUserInfoAsync(DefaultAccessTokenModel accessTokenModel)
    {
        var userInfoModel = await HttpRequestApi.GetAsync<GithubUserModel>(
            UserInfoUrl,
            BuildGetUserInfoParams(accessTokenModel),
            new Dictionary<string, string>()
            {
                ["Authorization"] = $"token {accessTokenModel.AccessToken}"
            }
        );
        if (userInfoModel.HasError())
        {
            throw new System.Exception(userInfoModel.ErrorMessage);
        }
        return userInfoModel;
    }
}