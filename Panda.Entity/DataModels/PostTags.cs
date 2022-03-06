using System.ComponentModel.DataAnnotations;
using Panda.Admin.Entities.DataModels;

namespace Panda.Entity.DataModels;

public class PostTags : PandaBaseTable
{
    [Required, StringLength(20)] 
    public string TagName { get; set; }

    public int PostCount { get; set; }
}