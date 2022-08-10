using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Panda.Tools;

public static class AutoInject
{
    /// <summary>
    ///     自动注入程序集所有的接口
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static void AddAutoInject(this IServiceCollection serviceCollection, Action<AutoInjectOption> action)
    {
        var option = new AutoInjectOption();
        action(option);
        foreach (var item in option.Options)
        {
            var types = Assembly.GetCallingAssembly().GetTypes().Where(a => a.IsClass && a.IsPublic)
                .Where(a => a.Name.EndsWith(item.EndWdith));
            foreach (var type in types)
                if (item.InjectSelf == false)
                {
                    if (!type.Name.EndsWith(item.EndWdith) || type.GetInterfaces().Length <= 0 ||
                        !type.GetInterfaces().Any(a => a.Name.EndsWith(item.EndWdith))) continue;
                    var impInterface = type.GetInterfaces().FirstOrDefault();
                    Inject(serviceCollection, type, impInterface!);
                }
                else
                {
                    if (type.IsGenericType == false && type.IsInterface == false) Inject(serviceCollection, type, type);
                }
        }
    }

    private static void Inject(IServiceCollection serviceCollection, Type type, Type impInterface)
    {
        var att = type.GetCustomAttribute<AutoInjectScopeAttribute>();
        Console.WriteLine($"注入{type.FullName} -> {impInterface.FullName}");
        if (att != null)
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
        else
            serviceCollection.AddScoped(impInterface, type);
    }

    /// <summary>
    /// 自动扫描注入FluentValidation的规则
    /// </summary>
    /// <param name="services"></param>
    /// <param name="startName"></param>
    public static void AddScanFluentValidation(this IServiceCollection services, string startName = "Panda.")
    {
        var assemblyList = Assembly.GetEntryAssembly()?.GetReferencedAssemblies()
            .Where(a => string.IsNullOrWhiteSpace(a.Name) == false && a.Name.StartsWith(startName));
        if (assemblyList != null)
        {
            foreach (var assembly in assemblyList)
            {
                var validators = Assembly.Load(assembly).GetTypes()
                   .Where(type => type.GetInterfaces().Where(a => a == typeof(IValidator)).Any())
                   .Select(a => a).ToList();
                foreach (var validator in validators)
                {
                    var baseType = validator.BaseType;
                    if (baseType!.IsGenericType)
                    {
                        var args = baseType.GetGenericArguments();
                        var v = typeof(IValidator<>).MakeGenericType(args[0]);
                        Console.WriteLine("注入验证器:" + v.FullName + "   " + validator.FullName);
                        services.AddScoped(v, validator);
                    }
                }
            }
        }
    }
}

public class AutoInjectOption
{
    public readonly List<AutoInjectOptionItem> Options = new();
}

public class AutoInjectOptionItem
{
    /// <summary>
    ///     结尾
    /// </summary>
    public string EndWdith { get; set; }

    /// <summary>
    ///     是否是直接注入自己，不注入接口
    /// </summary>
    public bool InjectSelf { get; set; } = false;
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