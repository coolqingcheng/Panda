using System.ComponentModel.DataAnnotations;

namespace Panda.Admin.Models.Request
{
    public class EmailSendModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
