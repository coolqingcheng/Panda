using Microsoft.EntityFrameworkCore;
using Panda.Admin.DataModels;
using Panda.Tools.Auth;
using Panda.Tools.Auth.Models;

namespace Panda.Admin.Monitoring;

/// <summary>
/// 网站监控上下文
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonitoringContext<T> : AppContext<T> where T : Accounts
{
    public MonitoringContext(DbContextOptions option) : base(option)
    {
    }

    public DbSet<Monitorings> Monitorings { get; set; }

    public DbSet<MonitoringDetail> MonitoringDetails { get; set; }
}