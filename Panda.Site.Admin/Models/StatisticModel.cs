namespace Panda.Site.Admin.Models;

public class StatisticModel
{
    public int PostCount { get; set; }

    public int IpCount { get; set; }
}

public class RecentAccessHistory{
    /// <summary>
    /// 访客唯一ID
    /// </summary>
    public string UId { get; set; }


    /// <summary>
    /// 访问地址
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 访问IP
    /// </summary>
    public string IP { get; set; }


    public string? UA { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string? OS { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    public string? Browser { get; set; }


    /// <summary>
    /// referer
    /// </summary>
    public string? Referer { get; set; }
    
    
    /// <summary>
    ///     添加时间
    /// </summary>
    public DateTimeOffset AddTime { get; set; }


    /// <summary>
    /// Ip所属区域
    /// </summary>
    public string Region { get; set; }

}
