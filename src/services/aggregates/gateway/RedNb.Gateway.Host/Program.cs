using Microsoft.AspNetCore.Http.Features;
using RedNb.Gateway.Host;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppSettingsSecretsJson()
    .UseAutofac();

builder.Services.AddApplication<GatewayHostModule>();

var app = builder.Build();

app.InitializeApplication();

app.Run();