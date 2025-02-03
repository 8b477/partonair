using API.partonair.CustomExceptions;

using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.Queries.Users;
using ApplicationLayer.partonair.Services;
using DomainLayer.partonair.Contracts;
using InfrastructureLayer.partonair.Repositories;

namespace API.partonair.DependencyInjection
{
    public static class DependencyInjectionManager
    {
        public static void ServicesHandler(this IServiceCollection services)
        {
            // USER
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            // UNIT OF WORK
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // MEDIATR
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetUserByIdHandler).Assembly));

            // Problem Details format more at
            services.AddProblemDetails();
            services.AddExceptionHandler<CustomExceptionHandler>();

            // Add services to the container.
            services.AddControllers();

            // Swagger/OpenAPI more at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
