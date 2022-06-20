using System.ComponentModel.DataAnnotations;

namespace Panda.Admin.Models.Request;

public class CreateAdminAccountRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Pwd { get; set; }
}