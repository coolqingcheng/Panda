using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class AccessStatistic : PandaBaseTable
{
    /// <summary>
    /// 访客唯一ID
    /// </summary>
    [StringLength(36)]
    public string UId { get; set; }


    /// <summary>
    /// 访问地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 访问IP
    /// </summary>
    [StringLength(50)]
    public string IP { get; set; }


    public string? UA { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    [StringLength(30)]
    public string? OS { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    [StringLength(30)]
    public string? Browser { get; set; }


    /// <summary>
    /// referer
    /// </summary>
    public string? Referer { get; set; }


    /// <summary>
    /// 访问者名称
    /// </summary>
    [StringLength(20)]
    public string? AccessName { get; set; }
    
}