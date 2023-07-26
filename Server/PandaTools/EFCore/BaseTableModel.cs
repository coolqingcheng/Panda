namespace PandaTools.EFCore;

public class BaseTableModel<T> : BaseTimeTable where T : struct
{
    public T Id { get; set; }

}

public class BaseTimeTable
{
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
}

public class SoftDeleteBaseTable<T> : BaseTableModel<T> where T : struct
{
    public bool IsDelete { get; set; }
}