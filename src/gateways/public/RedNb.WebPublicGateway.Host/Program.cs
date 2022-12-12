using Microsoft.AspNetCore.Http.Features;
using RedNb.WebGateway.Host;

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

builder.Services.AddApplication<WebGatewayHostModule>();

var app = builder.Build();

app.InitializeApplication();

app.Run();