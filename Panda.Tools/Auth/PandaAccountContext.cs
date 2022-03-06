using Microsoft.EntityFrameworkCore;
using Panda.Tools.Auth.Models;

namespace Panda.Tools.Auth;

public abstract class PandaAccountContext<TU> : DbContext where TU : Accounts
{
    public DbSet<TU> Accounts { get; set; }

    public PandaAccountContext(DbContextOptions option) : base(option)
    {
    }
}