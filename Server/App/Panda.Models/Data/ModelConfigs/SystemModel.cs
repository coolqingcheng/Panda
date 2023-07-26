

namespace Panda.Models.Data.ModelConfigs
{
    public static class SystemModel
    {
        public static void SystemModelConfig(this ModelBuilder builder)
        {
            builder.SetDateTimeConfig<SysConfig>().Property(a => a.Key).HasMaxLength(50);
            builder.SetDateTimeConfig<SysConfig>().Property(a => a.Value).HasMaxLength(2000);
            builder.SetDateTimeConfig<SysConfig>().Property(a => a.Description).HasMaxLength(200);
            builder.SetDateTimeConfig<SysConfig>().HasIndex(a => a.Key).IsUnique();
            builder.SetDateTimeConfig<SysConfig>().Property(a => a.GroupName).HasMaxLength(50);
            builder.SetDateTimeConfig<AccountLoginRecord>().Property(a => a.Ip).HasMaxLength(50);
            builder.SetDateTimeConfig<AccountLoginRecord>().Property(a => a.UA).HasMaxLength(500);
            builder.SetDateTimeConfig<SysResource>();
        }
    }
}
