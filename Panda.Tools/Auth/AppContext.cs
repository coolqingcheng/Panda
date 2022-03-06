using Microsoft.EntityFrameworkCore;
using Panda.Tools.Auth.Models;

namespace Panda.Tools.Auth;

public abstract class AppContext<TU> : DbContext where TU : Accounts
{
    public DbSet<TU> Accounts { get; set; }
    
    public DbSet<DicDatas> DicDatas { get; set; }

    public AppContext(DbContextOptions option) : base(option)
    {
        
    }
}