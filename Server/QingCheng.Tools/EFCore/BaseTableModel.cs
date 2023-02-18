using QingCheng.Tools.EFCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Tools.EFCore;

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