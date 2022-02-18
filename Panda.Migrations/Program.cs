using System;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;

Console.WriteLine(args[0]);
Console.WriteLine("begin migration:");
var optionsBuilder = new DbContextOptionsBuilder<PandaContext>();
optionsBuilder.UseMySql(args[0], ServerVersion.AutoDetect(args[0]));
var context = new PandaContext(optionsBuilder.Options);
await context.Database.MigrateAsync();
Console.WriteLine("migration success !!!");