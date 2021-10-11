using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace Panda.Entity.DataModels;

public class PandaBaseTable
{
    [Key]
    public int Id { get; set; }

    public DateTime AddTime { get; set; }
}