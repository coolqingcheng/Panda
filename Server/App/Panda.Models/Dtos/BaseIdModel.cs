using System.ComponentModel.DataAnnotations;

namespace Panda.Models.Dtos;

public class BaseIdModel<T>
{
    [Required]
    public T Id { get; set; }
}