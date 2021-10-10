using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Panda.Tools;

public static class AutoInject
{
    /// <summary>
    /// 自动注入程序集所有的接口
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static void AddAutoInject(this IServiceCollection serviceCollection, Action<AutoInjectOption> action)
    {
        var option = new AutoInjectOption();
        action(option);
        foreach (var (key, value) in option.AssemblyStringList)
        {
            var types  = Assembly.Load(key).GetTypes().Where(a => a.IsClass && a.IsPublic);
            foreach (var type in types)
            {
                if (!type.Name.EndsWith(value) || type.GetInterfaces().Length <= 0 ||
                    !type.GetInterfaces()[0].Name.EndsWith(value)) continue;
                var att = type.GetCustomAttribute<AutoInjectScopeAttribute>();
                var impInterface = type.GetInterfaces()[0];
#if DEBUG
                Console.WriteLine($"正在注入:{impInterface.FullName}->{type.FullName}");
#endif
                if (att != null)
                {
                    switch (att)
                    {
                        case { InjectType: AutoInjectType.Scope }:
                            serviceCollection.AddScoped(impInterface, type);
                            break;
                        case { InjectType: AutoInjectType.Single }:
                            serviceCollection.AddSingleton(impInterface, type);
                            break;
                        case { InjectType: AutoInjectType.Transient }:
                            serviceCollection.AddTransient(impInterface, type);
                            break;
                    }
                }
                else
                {
                    serviceCollection.AddScoped(impInterface, type);
                }
            }
        }
    }
}

public class AutoInjectOption
{
    private Dictionary<string, string> _assemblyStringList = new Dictionary<string, string>();

    /// <summary>
    /// 启动注入配置
    /// key:类库名称 value: 以什么为后缀的类与接口可以自动注入
    /// </summary>
    public Dictionary<string, string> AssemblyStringList
    {
        get => _assemblyStringList;
        set => _assemblyStringList = value;
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class AutoInjectScopeAttribute : Attribute
{
    public AutoInjectType InjectType { get; set; } = AutoInjectType.Scope;
}

public enum AutoInjectType
{
    Scope,
    Single,
    Transient
}