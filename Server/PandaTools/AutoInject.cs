﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace PandaTools;

public static class AutoInject
{
    /// <summary>
    ///     自动注入
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="assembly"></param>
    /// <param name="suffix"></param>
    public static void InjectSuffix(this IServiceCollection collection, Assembly? assembly, string suffix = "Service")
    {
        if (assembly == null)
        {
            Console.WriteLine("注入的type为空");
            return;
        }

        var types = assembly.GetTypes();
        var services = types.Where(a => a.Name.EndsWith(suffix)).Where(a => a.IsAbstract == false).Where(a => a.IsClass)
            .Where(a => a.GetCustomAttribute<IgnoreInjectAttribute>(false) == null).ToList();
        foreach (var service in services)
        {
#if DEBUG
            Console.WriteLine("注入:" + service.FullName);
#endif
            var injectType = service.GetType().GetCustomAttribute<InjectAttribute>();
            if (injectType == null || injectType.Type == InjectType.Scope)
                collection.TryAddScoped(service);
            else if (injectType.Type == InjectType.Single)
                collection.TryAddSingleton(service);
            else
                collection.TryAddTransient(service);
        }
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class IgnoreInjectAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class)]
public class InjectAttribute : Attribute
{
    public InjectAttribute(InjectType type)
    {
        Type = type;
    }

    public InjectType Type { get; set; }
}

public enum InjectType
{
    Scope,
    Single,
    Transient
}