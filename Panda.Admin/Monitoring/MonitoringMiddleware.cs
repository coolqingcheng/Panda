using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Panda.Admin.Attributes;
using Panda.Tools.Extensions;

namespace Panda.Admin.Monitoring;

public class MonitoringMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    private readonly IMonitoringService _monitoringService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public MonitoringMiddleware(RequestDelegate requestDelegate, IMonitoringService monitoringService,
        IHttpContextAccessor httpContextAccessor)
    {
        _requestDelegate = requestDelegate;
        _monitoringService = monitoringService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Invoke(HttpContext context)
    {
        var monitoring = context.GetEndpoint()?.Metadata.GetMetadata<MonitoringAttribute>();
        await _requestDelegate.Invoke(context);
        if (monitoring != null)
        {
            var model = new MonitoringModel()
            {
                UA = context.Request.Headers.UserAgent,
                Ip = _httpContextAccessor.HttpContext.GetClientIp(),
                Url = _httpContextAccessor.GetCompleteUrl(),
                CreateTime = DateTime.Now
            };
            context.Response.OnCompleted(() => _monitoringService.SaveAsync(model));
        }
    }
}