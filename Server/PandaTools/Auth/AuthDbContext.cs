using Microsoft.EntityFrameworkCore;
using PandaTools.Auth.Models;

namespace PandaTools.Auth;

public class BaseDbContext<User> : DbContext where User : BaseAccount
{
    public BaseDbContext(DbContextOptions option) : base(option)
    {
    }

    public virtual DbSet<User> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        #region 批量设置CreateTime

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType.ClrType.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "CreateTime" && property.PropertyType == typeof(DateTime))
                {
                    modelBuilder.Entity(entityType.ClrType).Property(property.Name).HasDefaultValueSql("now()");
                }
            }
        }

        #endregion
    }
}