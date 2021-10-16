using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;

namespace Panda.Entity;

public class PandaContext : DbContext
{
    public DbSet<Accounts> Accounts { get; set; }

    public DbSet<Articles> Articles { get; set; }

    public DbSet<Categorys> Categories { get; set; }

    public DbSet<ArticleCategoryRelations> ArticleCategoryRelations { get; set; }

    public DbSet<AuditLogs> AuditLogs { get; set; }

    public PandaContext(DbContextOptions<PandaContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Accounts>().HasIndex(a => a.UserName).IsUnique();
    }
}