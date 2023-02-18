﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QingCheng.Tools.Auth.Models;

namespace QingCheng.Tools.Auth;

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