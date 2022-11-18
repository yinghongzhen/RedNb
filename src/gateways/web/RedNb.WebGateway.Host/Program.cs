using RedNb.WebGateway.Host;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddAppSettingsSecretsJson();
builder.Host.UseAutofac();

builder.Services.AddApplication<WebGatewayHostModule>();

var app = builder.Build();

app.InitializeApplication();

app.Run();