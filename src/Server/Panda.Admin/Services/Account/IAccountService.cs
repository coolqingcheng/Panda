using Panda.Admin.Models;
using Panda.Admin.Models.Accounts;
using Panda.Admin.Models.Request;
using Panda.Entity.Responses;
using Panda.Tools.Auth;
using Panda.Tools.Auth.Models;
using Panda.Tools.Auth.Request;
using Panda.Tools.Auth.Response;

namespace Panda.Admin.Services.Account;

public interface IAccountService
{
    // Task InitAsync();

    /// <summary>
    ///     登录
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<AuthResult> LoginAsync(string email, string password);

    /// <summary>
    ///     修改密码
    /// </summary>
    /// <param name="oldPwd"></param>
    /// <param name="newPwd"></param>
    /// <returns></returns>
    Task ChangePwdAsync(ChangePwdRequest request);


    /// <summary>
    ///     初始化一个后台管理员账号
    /// </summary>
    /// <returns></returns>
    Task CreateAdminAccount(CreateAdminAccountRequest request);


    /// <summary>
    ///     禁用账号
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    Task Disable(Guid accountId, bool status);


    /// <summary>
    ///     获取当前用户
    /// </summary>
    /// <returns></returns>
    Task<TU?> GetCurrentAccount<TU>() where TU : Accounts, new();

    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    Task<PageDto<AccountResp>> GetAccountList(AccountReq req);

    /// <summary>
    /// 检查当前用户表是否已经初始化
    /// </summary>
    /// <returns></returns>
    Task<bool> CheckAdminAccountExistAsync();

    /// <summary>
    /// 创建账户
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task CreateAccount(CreateAccountModel model);
    /// <summary>
    /// 编辑账户
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task EditAccount(CreateAccountModel model);


    /// <summary>
    /// 检查用户是否是超级用户
    /// </summary>
    /// <param name="AccountId"></param>
    /// <returns></returns>
    Task<bool> IsAdmin(Guid AccountId);
}