using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class PandaBaseTable
{
    [Key]
    public int Id { get; set; }

    public DateTime AddTime { get; set; }
}