using System.Text;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Panda.Tools.Security;

namespace Panda.Tools.Aop;

public class CacheAttribute : AbstractInterceptorAttribute
{
    private int _second { get; set; }

    public CacheAttribute(int second)
    {
        _second = second;
    }

    public override async Task Invoke(AspectContext context, AspectDelegate next)
    {
        var distributedCache = context.ServiceProvider.GetService<IDistributedCache>();

        var json = JsonConvert.SerializeObject(context.Parameters);
        string parameterMd5 = Md5Helper.ComputeHash(json);
        var key =
            $"AopCache_{context.ImplementationMethod.DeclaringType?.FullName}_{context.ImplementationMethod.Name}_{parameterMd5}";
        var cacheValue = await distributedCache.GetStringAsync(key);
        if (string.IsNullOrWhiteSpace(cacheValue))
        {
            await next(context);
            if (context.IsAsync())
            {
                var value = await context.UnwrapAsyncReturnValue();
                cacheValue = JsonConvert.SerializeObject(value);
            }
            else
            {
                cacheValue = JsonConvert.SerializeObject(context.ReturnValue);
            }

            await distributedCache.SetStringAsync(key, cacheValue, new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromSeconds(_second)
            });
        }
        else
        {
           
            Console.WriteLine("缓存里面获取:" + key);
            var v = JsonConvert.DeserializeObject(cacheValue);
            context.ReturnValue = GetReturnValue(cacheValue, context.ProxyMethod.ReturnType,
                context.ServiceMethod.IsReturnTask());
            await context.Break();
        }
    }

    object GetReturnValue(string json, Type returnType, bool isAsync)
    {
        if (isAsync)
        {
            if (returnType.GetGenericArguments().Any())
            {
                var data = JsonConvert.DeserializeObject(json, returnType.GenericTypeArguments.FirstOrDefault());
                return Task.FromResult(data);
            }
            else
            {
                return Task.CompletedTask;
            }
        }
        else
        {
            return JsonConvert.DeserializeObject(json, returnType);
            ;
        }
    }
}