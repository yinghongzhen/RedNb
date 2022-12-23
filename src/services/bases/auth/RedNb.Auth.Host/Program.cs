using Microsoft.AspNetCore.Http.Features;
using RedNb.Auth.Host;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppSettingsSecretsJson()
    .UseAutofac();

builder.Services.AddApplication<AuthHostModule>();

var app = builder.Build();

app.InitializeApplication();

app.Run();