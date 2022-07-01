using System.Reflection;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Controllers;

namespace Panda.Tools.Auth.Permission.Scan;

public interface IPermissionScan
{
}

public class PermissionScan : IPermissionScan
{
    private readonly Dictionary<string, HashSet<string>> _dictionary = new();

    public PermissionScan()
    {
        var dir = AppContext.BaseDirectory;
        var files = Directory.GetFiles(dir, "Panda.*.dll");
        foreach (var file in files)
        {
            var controllers = Assembly.LoadFile(file).GetTypes()
                .Where(type => typeof(AdminController).IsAssignableFrom(type))
                .Select(a => a);
            var list = controllers.Count();
            foreach (var controller in controllers)
            {
                var group = controller.GetCustomAttribute<PermissionGroupAttribute>();
                if (group != null)
                {
                    Console.WriteLine("group:" + group.Name);
                    var methods =
                        controller.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic |
                                              BindingFlags.Instance).Where(a =>
                            a.GetCustomAttribute<PermissionAttribute>() != null).ToList();
                    if (_dictionary.TryGetValue(group.Name, out var set) == false)
                    {
                        set = new HashSet<string>();
                        _dictionary.TryAdd(group.Name, set);
                    }

                    foreach (var method in methods)
                    {
                        var name = method.GetCustomAttribute<PermissionAttribute>()!.Name;
                        Console.WriteLine("method:" + name);
                        set?.Add(name);
                    }
                }
            }
        }
    }
}