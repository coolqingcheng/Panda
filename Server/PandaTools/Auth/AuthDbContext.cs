using Microsoft.EntityFrameworkCore;
using PandaTools.Auth.Models;

namespace PandaTools.Auth;

public class AuthDbContext<User> : DbContext where User : BaseAccount
{
    public virtual DbSet<User> Accounts { get; set; }

    public AuthDbContext(DbContextOptions option) : base(option)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}