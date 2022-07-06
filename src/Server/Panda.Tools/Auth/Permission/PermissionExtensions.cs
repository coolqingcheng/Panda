using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Permission.Models;
using Panda.Tools.Auth.Permission.Scan;
using Panda.Tools.Web;
using System.Net;
using System.Security.Claims;

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

    public PermissionMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = GetEndpoint(context);
        var logger = context.RequestServices.GetService<ILogger<PermissionMiddleware>>();
        if (endpoint != null)
        {
            var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            var permissionGroup = controllerActionDescriptor?.ControllerTypeInfo.GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(PermissionGroupAttribute));
            var permission = endpoint.Metadata.GetMetadata<PermissionAttribute>();
            var allowAnony = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>();
            var NoPermission = endpoint.Metadata.GetMetadata<NoPermissionAttribute>();
            var isAdmin = App.IsAdmin();
            if (allowAnony != null || NoPermission != null || controllerActionDescriptor == null || isAdmin)
            {
                await _request.Invoke(context);
            }
            else
            {
                if (permission == null || permissionGroup == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
                else
                {
                    var groupName = ((PermissionGroupAttribute)permissionGroup).Name;
                    var permissionName = permission.Name;
                    logger?.LogInformation($"正在验证权限:{groupName}-{permission.Name}");
                    var accountId = App.GetAccountId();
                    var permissionChecker = context.RequestServices.GetService<IPermissionUtils>();
                    var isGrant = await permissionChecker!.ChecPermission(accountId, groupName, permission.Name);
                    if (isGrant)
                    {
                        await _request.Invoke(context);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    }
                }
            }
        }
        else
        {
            await _request.Invoke(context);
        }

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