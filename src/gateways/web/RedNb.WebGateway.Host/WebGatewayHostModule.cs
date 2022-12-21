using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using RedNb.Core;
using RedNb.Core.Web;
using RedNb.WebGateway.Host.Middlewares;
using System.Text.Json;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace RedNb.WebGateway.Host;

public class CutomerTransformProvider : ITransformProvider
{
    public void Apply(TransformBuilderContext context)
    {
        context.AddRequestTransform(transformContext => {
            var pathArr = transformContext.Path.Value.Split("/").Where(b => !string.IsNullOrEmpty(b)).Select(b => b).ToList();
            
            string apiPath = "";
            switch (pathArr[0])
            {
                case "auth":
                    pathArr[0] = "api/app";
                    break;
                default:
                    break;
            }
            transformContext.Path = "/" + string.Join("/", pathArr);
            return new ValueTask();
        });
    }

    public void ValidateCluster(TransformClusterValidationContext context)
    {
        //throw new NotImplementedException();
    }

    public void ValidateRoute(TransformRouteValidationContext context)
    {
        //throw new NotImplementedException();
    }
}

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAutofacModule),
    typeof(AbpSwashbuckleModule),
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
            options.Filters.AddService(typeof(DefaultAbpExceptionFilter), 20);
        });

        Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new DefaultJsonConverter());
        });

        Configure<AbpAntiForgeryOptions>(options => { options.AutoValidate = false; });

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

        context.Services.AddReverseProxy()
            //.LoadFromMemory(routes, clusters)
            .LoadFromConfig(configuration.GetSection("ReverseProxy"))
            .AddTransforms<CutomerTransformProvider>();
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
