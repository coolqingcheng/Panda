using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.Requests;

public class PageRequest
{
    [Required] public int Index { get; set; } = 1;

    [Required]
    public int Size { get; set; } = 10;
}