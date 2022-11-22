

using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RedNb.Core.Web;
using RedNb.WebGateway.Application.Contracts;
using RedNb.WebGateway.Domain;
using RedNb.WebGateway.Domain.Shared;
using StackExchange.Redis;
using System.Text.Json;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Caching;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Localization;
using Volo.Abp.VirtualFileSystem;

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

        context.Services.AddControllersWithViews();

        //Configure<MvcOptions>(mvcOptions =>
        //{
        //    mvcOptions.Filters.AddService(typeof(DefaultAbpExceptionFilter), 20);
        //});

        Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new DefaultJsonConverter());
        });

        Configure<AbpAntiForgeryOptions>(options =>
        {
            options.AutoValidate = false;
        });

        ConfigureConventionalControllers();
        ConfigureLocalization();
        ConfigureCache(configuration);
        ConfigureVirtualFileSystem(context);
        ConfigureDistributedLocking(context, configuration);
        ConfigureCors(context, configuration);
        ConfigureSwaggerServices(context, configuration);
    }

    private void ConfigureCache(IConfiguration configuration)
    {
        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "WebGateway:"; });
    }

    private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<WebGatewayDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}RedNb.WebGateway.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<WebGatewayDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}RedNb.WebGateway.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<WebGatewayApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}RedNb.WebGateway.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<WebGatewayApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}RedNb.WebGateway.Application"));
            });
        }
    }

    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(WebGatewayApplicationModule).Assembly);
        });
    }

    private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
    {
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
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
        });
    }

    private void ConfigureDistributedLocking(
        ServiceConfigurationContext context,
        IConfiguration configuration)
    {
        context.Services.AddSingleton<IDistributedLockProvider>(sp =>
        {
            var connection = ConnectionMultiplexer
                .Connect(configuration["Redis:Configuration"]);
            return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
        });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        //context.Services.AddCors(options =>
        //{
        //    options.AddDefaultPolicy(builder =>
        //    {
        //        builder
        //            .WithOrigins(
        //                configuration["App:CorsOrigins"]
        //                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
        //                    .Select(o => o.RemovePostFix("/"))
        //                    .ToArray()
        //            )
        //            .WithAbpExposedHeaders()
        //            .SetIsOriginAllowedToAllowWildcardSubdomains()
        //            .AllowAnyHeader()
        //            .AllowAnyMethod()
        //            .AllowCredentials();
        //    });
        //});
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebGateway API");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
