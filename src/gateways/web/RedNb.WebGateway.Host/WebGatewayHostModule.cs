

using Autofac.Core;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RedNb.Core.Web;
using RedNb.WebGateway.Application.Contracts;
using RedNb.WebGateway.Domain;
using RedNb.WebGateway.Domain.Shared;
using RedNb.WebGateway.Host.Extensions;
using StackExchange.Redis;
using System.Text.Json;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Caching;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Json;
using Volo.Abp.Localization;
using Volo.Abp.VirtualFileSystem;
using Yarp.ReverseProxy.Configuration;

namespace RedNb.WebGateway.Host;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAutofacModule),
    typeof(AbpDistributedLockingModule),
    typeof(AbpSwashbuckleModule),
    typeof(WebGatewayApplicationModule),
    typeof(WebGatewayEntityFrameworkCoreModule),
    typeof(CoreModule)
)]
public class WebGatewayHostModule : AbpModule
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
            foreach (var item in options.Filters)
            {
                if(item is ServiceFilterAttribute)
                {
                    var type = (ServiceFilterAttribute)item;
                    Console.WriteLine(type.ServiceType);
                }
            }

            //options.Filters.AddService(typeof(DefaultAbpExceptionFilter), 20);
        });

        Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new DefaultJsonConverter());
        });

        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(WebGatewayApplicationModule).Assembly);
        });

        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "WebGateway:"; });

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
                        Path = "{**catch-all}"
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
                        { "destination1", new DestinationConfig() { Address = "https://www.baidu.com" } }
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
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebGateway API");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapReverseProxy();
        });
    }
}
