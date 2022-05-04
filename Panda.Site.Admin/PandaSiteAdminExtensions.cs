using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Panda.Tools;
using Panda.Tools.CSRF;
using Panda.Tools.Filter;
using Panda.Tools.Web;

namespace Panda.Site.Admin;

public static class PandaSiteAdminExtensions
{
    public static void AddSiteAdmin(this IServiceCollection services)
    {
        services.AddAutoInject(opt =>
        {
            opt.Options.Add(new AutoInjectOptionItem()
            {
                EndWdith = "Service"
            });
        });
    }
}