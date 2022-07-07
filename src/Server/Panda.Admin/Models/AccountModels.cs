using FluentValidation;
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
    public Guid? Id { get; set; }
    [Required] public string OldPwd { get; set; }

    [Required] public string NewPwd { get; set; }
}

public class ChangeAccountPasswordRequest
{
    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public string NewPassword { get; set; }
}

public class ChangeAccountPasswordValidator : AbstractValidator<ChangeAccountPasswordRequest>
{
    public ChangeAccountPasswordValidator()
    {
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8).WithMessage("密码不能小于8位数");
    }
}