﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        foreach (var item in option.AssemblyStringList)
        {
            var types = Assembly.Load(item.AssemblyName).GetTypes().Where(a => a.IsClass && a.IsPublic);
            foreach (var type in types)
            {
                if (item.InjectSelf == false)
                {
                    if (!type.Name.EndsWith(item.EndWdith) || type.GetInterfaces().Length <= 0 ||
                        !type.GetInterfaces()[0].Name.EndsWith(item.EndWdith)) continue;
                    var impInterface = type.GetInterfaces()[0];
                    Inject(serviceCollection, type, impInterface);
                }
                else
                {
                    if (type.IsGenericType == false)
                        Inject(serviceCollection, type, type);
                }
            }
        }
    }

    private static void Inject(IServiceCollection serviceCollection, Type type, Type impInterface)
    {
        var att = type.GetCustomAttribute<AutoInjectScopeAttribute>();
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

public class AutoInjectOption
{
    public readonly List<AutoInjectOptionItem> AssemblyStringList = new();
}

public class AutoInjectOptionItem
{
    /// <summary>
    /// 名称
    /// </summary>
    public string AssemblyName { get; set; }

    /// <summary>
    /// 结尾
    /// </summary>
    public string EndWdith { get; set; }

    /// <summary>
    /// 是否是直接注入自己，不注入接口
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