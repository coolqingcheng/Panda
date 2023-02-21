using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace QingCheng.Tools
{
    public static class AutoInject
    {
        public static void InjectServices(this IServiceCollection collection, Assembly? assembly)
        {
            if (assembly == null)
            {
                Console.WriteLine("注入的type为空");
                return;
            }
            var types = assembly.GetTypes();
            var services = types.Where(a => a.Name.EndsWith("Service")).Where(a => a.IsAbstract == false).Where(a => a.IsClass).Where(a => a.GetCustomAttribute<IgnoreInjectAttribute>(inherit: false) == null).ToList();
            foreach (var service in services)
            {
#if DEBUG
                Console.WriteLine("注入:" + service.FullName);
#endif
                var injectType = service.GetType().GetCustomAttribute<InjectAttribute>();
                if (injectType == null || injectType.Type == InjectType.Scope)
                {
                    collection.TryAddScoped(service);
                }
                else if (injectType.Type == InjectType.Single)
                {
                    collection.TryAddSingleton(service);
                }
                else
                {
                    collection.TryAddTransient(service);
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class IgnoreInjectAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
        public InjectType Type { get; set; }

        public InjectAttribute(InjectType type)
        {
            Type = type;
        }
    }

    public enum InjectType
    {
        Scope, Single, Transient
    }

}
