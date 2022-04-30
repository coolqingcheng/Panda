using COSXML.Model.Object;

namespace Panda.Tools.Helper;

public class DateTimeHelper
{
    /// <summary>
    /// 获取本周一的时间
    /// </summary>
    /// <returns></returns>
    public static DateTime GetThisWeekMonday()
    {
        var date = DateTime.Now;
        var firstDate = date.DayOfWeek switch
        {
            DayOfWeek.Monday => date,
            DayOfWeek.Tuesday => date.AddDays(-1),
            DayOfWeek.Wednesday => date.AddDays(-2),
            DayOfWeek.Thursday => date.AddDays(-3),
            DayOfWeek.Friday => date.AddDays(-4),
            DayOfWeek.Saturday => date.AddDays(-5),
            DayOfWeek.Sunday => date.AddDays(-6),
            _ => DateTime.Now
        };

        return firstDate;
    }

    /// <summary>
    /// 获取本月第一天
    /// </summary>
    /// <returns></returns>
    public static DateTime GetFirstDayOfTheMonth()
    {
        return DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
    }
}