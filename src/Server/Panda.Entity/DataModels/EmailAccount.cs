using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class EmailAccount : PandaBaseTable
{
    [Required] [StringLength(100)] public string Email { get; set; }
}