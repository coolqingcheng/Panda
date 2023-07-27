using Microsoft.EntityFrameworkCore;
using PandaTools.Auth.Models;

namespace PandaTools.Auth;

public class AuthDbContext<User> : DbContext where User : BaseAccount
{
    public AuthDbContext(DbContextOptions option) : base(option)
    {
    }

    public virtual DbSet<User> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}