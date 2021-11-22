using System.ComponentModel.DataAnnotations;

namespace Panda.Models;

public class UploadBase64Model
{
    [Required]
    public string Base64 { get; set; }

    /// <summary>
    /// 0����д���
    /// </summary>
    public int W { get; set; } = 0;
    public int H { get; set; } = 0;
}