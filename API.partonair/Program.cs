using API.partonair.GlobalManager;

using InfrastructureLayer.partonair.Persistence;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Si vous voulez désactiver HTTPS en développement
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.HttpsPort = null;
    });
}


builder.Services
                .SqlServerConnectionManager(builder.Configuration)
                .AddInfrastructureLayer()
                .AddApplicationLayer()
                .AddPresentationAPILayer();


var app = builder.Build();

app
    .ConfigureExceptionHandling()
    .ConfigureDevelopmentMiddleware()
    .ConfigureHttpPipeline()
    .ConfigureRouting()
    .Run();
