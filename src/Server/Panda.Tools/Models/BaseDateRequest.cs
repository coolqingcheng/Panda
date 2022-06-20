using Panda.Tools.Helper;

namespace Panda.Tools.Models;

public class BaseDateRequest
{
    public DateType DateType { get; set; }

    public DateTime? Begin { get; set; } 

    public DateTime? End { get; set; } 

    public void BuildDate()
    {
        switch (DateType)
        {
            case DateType.ToDay:
                Begin = DateTime.Now.Date;
                End = DateTime.Now.AddDays(1).Date;
                break;
            case DateType.YesterDay:
                Begin = DateTime.Now.AddDays(-1).Date;
                End = DateTime.Now.Date;
                break;
            case DateType.ThisWeek:
                Begin = DateTimeHelper.GetThisWeekMonday().Date;
                End = DateTime.Now.AddDays(1).Date;
                break;
            case DateType.ThisMonth:
                Begin = DateTimeHelper.GetFirstDayOfTheMonth();
                End = DateTime.Now.AddDays(1).Date;
                break;
            case DateType.Custom:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum DateType
{
    /// <summary>
    /// 自定义
    /// </summary>
    Custom = 0,

    /// <summary>
    /// 今天
    /// </summary>
    ToDay = 1,

    /// <summary>
    /// 昨天
    /// </summary>
    YesterDay = 2,

    /// <summary>
    /// 本周
    /// </summary>
    ThisWeek = 3,

    /// <summary>
    /// 本月
    /// </summary>
    ThisMonth = 4
}