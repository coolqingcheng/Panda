using Panda.Tools.Models;

namespace Panda.SiteStatistic.Models;

public class SiteStatisticModel
{
    public int Pv { get; set; }

    public int Uv { get; set; }

    public int Ip { get; set; }

    /// <summary>
    /// 日期名称
    /// </summary>
    public string TimeName { get; set; }
}


public class SiteStatisticRequest : BaseDateRequest
{
    
}