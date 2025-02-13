using API.partonair.GlobalManager;

using InfrastructureLayer.partonair.Persistence;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Temporaire pour Docker
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer("Server=db;Database=partonair;User=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;Encrypt=False;MultipleActiveResultSets=True"));


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
