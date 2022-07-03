using System.ComponentModel.DataAnnotations;

namespace Panda.Admin.Admin.Models.Permission
{
    public class AccountSetPermissionRequest
    {
        [Required]
        public Guid AccountId { get; set; }

        public List<string> PermissionKeys { get; set; }
    }
}
