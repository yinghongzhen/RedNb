using Microsoft.AspNetCore.Http.Features;
using RedNb.Gateway.Host;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = int.MaxValue;
});

builder.Host
    .AddAppSettingsSecretsJson()
    .UseAutofac();

builder.Services.AddApplication<GatewayHostModule>();

var app = builder.Build();

app.InitializeApplication();

app.Run();