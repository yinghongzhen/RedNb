

using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RedNb.Core.Web;
using RedNb.Gateway.Host.Extensions;
using RedNb.Gateway.Host.Middlewares;
using StackExchange.Redis;
using System.Text.Json;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Caching;
using Volo.Abp.DistributedLocking;
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

        context.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        context.Services.AddControllersWithViews(options =>
        {
            options.Filters.AddService(typeof(DefaultAbpExceptionFilter), 20);
        });

        Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new DefaultJsonConverter());
        });

        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(GatewayApplicationModule).Assembly);
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

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(o => true)
                .AllowCredentials();
            });
        });

        var routes = new[]
            {
                new RouteConfig()
                {
                    RouteId = "route1",
                    ClusterId = "cluster1",
                    Match = new RouteMatch
                    {
                        Path = "a1/{**catch-all}"
                    }
                }
            };
                var clusters = new[]
                {
                new ClusterConfig()
                {
                    ClusterId = "cluster1",
                    Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "destination1", new DestinationConfig() { Address = "http://localhost:5029/" } }
                    }
                }
            };

        context.Services.AddReverseProxy()
            .LoadFromMemory(routes, clusters)
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseForwardedHeaders();
        app.UseCors();
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

            endpoints.MapReverseProxy(proxyPipeline =>
            {
                proxyPipeline.UseMiddleware<RedNbAuthorizationMiddleware>();
            });
        });
    }
}
