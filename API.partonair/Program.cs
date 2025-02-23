using API.partonair.GlobalManager;

using InfrastructureLayer.partonair.Persistence;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Temporaire pour Docker
var useDockerConfig = Environment.GetEnvironmentVariable("USE_DOCKER_CONFIG");

if (useDockerConfig == "true")
{
	builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseSqlServer("Server=db;Database=partonair;User=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;Encrypt=False;MultipleActiveResultSets=True"));
}else{
    builder.Services.SqlServerConnectionManager(builder.Configuration);
}

builder.Services           
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
