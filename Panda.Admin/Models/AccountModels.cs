using System.ComponentModel.DataAnnotations;

namespace Panda.Admin.Models;

public class AccountModels
{
}

public class AccountLoginRequest
{
    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }
}

public class ChangePwdRequest
{
    [Required] public string OldPwd { get; set; }

    [Required] public string NewPwd { get; set; }
}