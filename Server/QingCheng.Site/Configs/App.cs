﻿using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using QingCheng.Tools.Auth;
using QingCheng.Tools.Swagger;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QingCheng.Tools.EFCore;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace QingCheng.Site.Configs;


public static class AppExtension
{

    public static void AddApiConfig(this IServiceCollection servies)
    {
        servies.AddControllers().AddNewtonsoftJson(
                options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorMessage = context.ModelState.FirstOrDefault(
                            m => m.Value is { ValidationState: ModelValidationState.Invalid })
                        .Value
                        ?.Errors.FirstOrDefault()
                        ?.ErrorMessage;
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    return new JsonResult(new
                    {
                        message = errorMessage
                    });
                };
            });
        servies.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = true;
            
        });
        servies.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());
    }

    public static void AddApp(this WebApplicationBuilder builder)
    {
        var service = builder.Services;
        service.AddScoped<TranContext>();
        service.AddMediatR(Assembly.GetEntryAssembly()!);
        service.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
        service.AddScoped<JwtHelper>();
        service.AddScoped<CookieAuthHelper>();
        service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        service.AddHttpClient();
        service.AddSwagger();
    }

    static void AddSwagger(this IServiceCollection service)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen(c =>
        {
            c.DocumentFilter<SwaggerEnumFilter>();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"需要传递一个jwt的token 例子: 'Bearer 12345abcdef'，如果验证失败，请先检查token是否正确",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

            var dllList = Directory.GetFiles(AppContext.BaseDirectory, "*.dll");

            foreach (var xmlFile in dllList)
            {
                var fileInfo = new FileInfo(xmlFile);
                var fileName = fileInfo.Name.Substring(0,
                    fileInfo.Name.LastIndexOf(fileInfo.Extension, StringComparison.Ordinal));
                var path = Path.Combine(AppContext.BaseDirectory, fileName + ".xml");
                if (File.Exists(path))
                {
                    c.IncludeXmlComments(path, true);
                }
            }
        });
        service.AddSwaggerGenNewtonsoftSupport(); // explicit opt-in - needs to be placed after AddSwaggerGen()

    }

}