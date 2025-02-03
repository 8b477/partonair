using API.partonair.DependencyInjection;

using InfrastructureLayer.partonair.Persistence;

using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

DependencyInjectionManager.ServicesHandler(builder.Services);

// Retrieve connection string SqlServer
string connectionString = builder.Configuration["ConnectionString:SqlServer"]
    ?? throw new InvalidOperationException("'ConnectionString:SqlServer' is empty.");

builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString));


var app = builder.Build();
// Converts unhandled exceptions into Problem Details responses
app.UseExceptionHandler();
// Returns the Problem Details response for (empty) non-successful responses
app.UseStatusCodePages();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
