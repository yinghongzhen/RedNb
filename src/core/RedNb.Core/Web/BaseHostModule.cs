using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Json;

namespace RedNb.Core.Web;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule)
)]
public class BaseHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.Services.AddControllersWithViews();

        Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true);
        Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);

        Configure<MvcOptions>(mvcOptions =>
        {
            mvcOptions.Filters.AddService(typeof(DefaultAbpExceptionFilter), 20);
        });

        Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new DefaultJsonConverter());
        });

        Configure<AbpJsonOptions>(options =>
        {
            options.DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        });

        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(DwcjApplicationModule).Assembly,
                o =>
                {
                    o.UseV3UrlStyle = true;
                });
        });

        Configure<AbpAntiForgeryOptions>(options =>
        {
            options.AutoValidate = false;
        });

        context.Services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc(configuration["Service:Version"], new OpenApiInfo
                {
                    Title = configuration["Service:Name"],
                    Version = configuration["Service:Version"],
                    Description = configuration["Service:Description"],
                    License = new OpenApiLicense
                    {
                        Name = configuration["Service:License:Name"],
                        Url = new Uri(configuration["Service:License:Url"])
                    }
                });

                options.AddSecurityDefinition("token", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                options.DocInclusionPredicate((docName, description) => true);

                options.IncludeXmlComments(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "RedNb.Dwcj.Application.xml"));
            }
        );

        context.Services.AddExceptionless(c =>
        {
            c.ApiKey = configuration["Exceptionless:ApiKey"];
            c.ServerUrl = configuration["Exceptionless:ServerUrl"];
        });

        context.Services.AddRedis(o =>
        {
            o.Host = configuration["Redis:Host"];
            o.Port = Convert.ToInt32(configuration["Redis:Port"]);
            o.Password = configuration["Redis:Password"];
            o.Database = Convert.ToInt32(configuration["Redis:Database"]);
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
        var configuration = context.GetConfiguration();

        app.UseExceptionless();

        app.Use((context, next) =>
        {
            context.Request.EnableBuffering();
            return next();
        });

        app.UseStaticFiles();

        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/{configuration["Service:Version"]}/swagger.json", $"{configuration["Service:Name"]} {configuration["Service:Version"]}");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}