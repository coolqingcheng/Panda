using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaTools.EFCore;

public class BaseEntity<T>
{
    public T Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreateTime { get; set; }
}