using System.ComponentModel.DataAnnotations;

namespace Panda.Tools.Models;

public class BasePageRequest
{
    [Required] public int Index { get; set; } = 1;

    [Required] public int Size { get; set; } = 10;
}