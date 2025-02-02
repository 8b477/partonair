using API.partonair.CustomExceptions;

using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.Queries.Users;
using ApplicationLayer.partonair.Services;

using DomainLayer.partonair.Contracts;
using InfrastructureLayer.partonair.Persistence;
using InfrastructureLayer.partonair.Repositories;

using Microsoft.EntityFrameworkCore;

using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Retrieve connection string SqlServer
string connectionString = builder.Configuration["ConnectionString:SqlServer"]
    ?? throw new InvalidOperationException("La chaîne de connexion 'ConnectionString:SqlServer' est manquante dans la configuration.");

builder.Services.AddDbContext<AppDbContext>( o => o.UseSqlServer(connectionString));

// Add MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetUserByIdHandler).Assembly));

// Add dependency injection
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Adds services for using Problem Details format
builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

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
