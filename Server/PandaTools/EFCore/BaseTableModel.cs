namespace PandaTools.EFCore;

public class BaseEntity<T>
{
    public T Id { get; set; }

    public DateTime CreateTime { get; set; }
}