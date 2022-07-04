using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Permission.Models;
using Panda.Tools.Auth.Permission.Scan;
using Panda.Tools.Web;

namespace Panda.Tools.Auth.Permission;

public static class PermissionExtensions
{
    public static void UsePermission(this IApplicationBuilder app)
    {
        app.UseMiddleware<PermissionMiddleware>();
    }

    public static void UsePermission(this IServiceCollection collection)
    {
        collection.AddSingleton<IPermissionScan>(new PermissionScan());
        collection.AddScoped<IPermissionUtils, PermissionUtils>();
        collection.AddScoped<IPermissionContainer, PermissionContainer>();
    }
}

public class PermissionMiddleware
{
    private readonly RequestDelegate _request;

    private readonly IPermissionContainer _permissionContainer;

    public PermissionMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = GetEndpoint(context);
        if (endpoint != null)
        {
            var group = endpoint.Metadata.GetMetadata<PermissionGroupAttribute>();
            var permission = endpoint.Metadata.GetMetadata<PermissionAttribute>();
            if (permission != null)
            {
                Console.WriteLine($"访问:{permission.Name}");
            }
        }
        else
        {
        }

        await _request.Invoke(context);
    }

    public static Endpoint? GetEndpoint(HttpContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        return context.Features.Get<IEndpointFeature>()?.Endpoint;
    }
}