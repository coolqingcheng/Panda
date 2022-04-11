using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Panda.Tools.Auth;
using Panda.Tools.Auth.Models;

namespace Panda.Admin.DataModels;

public class Monitorings : BaseTable
{
    public string Url { get; set; }

    /// <summary>
    /// 详情
    /// </summary>
    public ICollection<MonitoringDetail> MonitoringDetails { get; set; }
}

public class MonitoringDetail : BaseTable
{
    [StringLength(20)]
    public string Ip { get; set; }

    [StringLength(20)]
    public string Os { get; set; }

    [StringLength(50)]
    public string Browser { get; set; }
    
    public string UA { get; set; }
}

public class MonitoringDbContext<TU> : AppContext<TU> where TU : Accounts
{
    public MonitoringDbContext(DbContextOptions option) : base(option)
    {
    }

    public DbSet<Monitorings> Monitorings { get; set; }

    public DbSet<MonitoringDetail> MonitoringDetails { get; set; }
}