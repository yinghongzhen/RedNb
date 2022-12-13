

using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RedNb.Core.Web;
using StackExchange.Redis;
using System.Text.Json;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Caching;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Localization;
using Yarp.ReverseProxy.Configuration;

namespace RedNb.Gateway.Host;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAutofacModule),
    typeof(AbpDistributedLockingModule),
    typeof(AbpSwashbuckleModule),
    typeof(GatewayApplicationModule),
    typeof(GatewayEntityFrameworkCoreModule),
    typeof(CoreModule)
)]
public class GatewayHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        //context.Services.AddControllersWithViews(options =>
        //{
        //    options.Filters.AddService(typeof(DefaultAbpExceptionFilter), 20);
        //});

        //Configure<JsonOptions>(options =>
        //{
        //    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        //    options.JsonSerializerOptions.Converters.Add(new DefaultJsonConverter());
        //});

        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(GatewayApplicationModule).Assembly);
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "��������"));
        });

        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Gateway:"; });

        Configure<AbpAntiForgeryOptions>(options => { options.AutoValidate = false; });

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

                    options.DocInclusionPredicate((docName, description) => true);

                    //options.IncludeXmlComments(Path.Combine(
                    //AppDomain.CurrentDomain.BaseDirectory,
                    //"RedNb.Dwcj.Application.xml"));
                }
            );

        context.Services.AddSingleton<IDistributedLockProvider>(sp =>
        {
            var connection = ConnectionMultiplexer
                .Connect(configuration["Redis:Configuration"]);
            return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        app.UseForwardedHeaders();

        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
