using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Models.Auth;

public class LoginModel
{
    /// <summary>
    /// 账号
    /// </summary>
    [Required]
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    public string Pwd { get; set; }
    /// <summary>
    /// 验证码
    /// </summary>
    public string ValidCode { get; set; }
}

public class LoginResp
{
    public string Token { get; set; }
}