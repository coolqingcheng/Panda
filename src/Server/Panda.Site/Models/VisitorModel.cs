using System.ComponentModel.DataAnnotations;

namespace Panda.Site.Models;

public class VisitorModel
{
    
}

public class VisitorSendVerificationCode
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Code { get; set; }

    [Required]
    public string NickName { get; set; }
}