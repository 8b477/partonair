using API.partonair.GlobalManager;


var builder = WebApplication.CreateBuilder(args);

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
