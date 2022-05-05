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
            context.IsAsync();
            if (context.IsAsync())
            {
                var value = await context.UnwrapAsyncReturnValue();
                cacheValue = JsonConvert.SerializeObject(value);
            }
            else
            {
                cacheValue = JsonConvert.SerializeObject(context.ReturnValue);
            }

            context.ReturnValue = cacheValue;
            await distributedCache.SetStringAsync(key, cacheValue, new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromSeconds(_second)
            });
        }
        else
        {
            Console.WriteLine("缓存里面获取:" + key);
            if (context.ServiceMethod.IsReturnTask())
            {
                context.ReturnValue = Task.FromResult(JsonConvert.DeserializeObject(cacheValue));
            }
            else
            {
                context.ReturnValue = JsonConvert.DeserializeObject(cacheValue);
            }
        }
    }
}