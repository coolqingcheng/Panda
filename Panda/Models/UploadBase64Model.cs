using System.ComponentModel.DataAnnotations;

namespace Panda.Models;

public class UploadBase64Model
{
    [Required]
    public string Base64 { get; set; }
}