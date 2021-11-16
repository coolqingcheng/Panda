using System.ComponentModel.DataAnnotations;

namespace Panda.Admin.Models;

public class AccountModels
{
    
}

public class AccountLoginRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}