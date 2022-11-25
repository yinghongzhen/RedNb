using RedNb.WebGateway.Host;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

//builder.Host.AddAppSettingsSecretsJson();
//builder.Host.UseAutofac();

//builder.Services.AddApplication<WebGatewayHostModule>();

var app = builder.Build();

app.MapReverseProxy();

//app.InitializeApplication();

app.Run();