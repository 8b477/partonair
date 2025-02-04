using API.partonair.CustomExceptions;

using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.MediatR.Queries.Users;
using ApplicationLayer.partonair.Services;

using DomainLayer.partonair.Contracts;

using InfrastructureLayer.partonair.Repositories;

namespace API.partonair.GlobalManager
{
    public static class DependencyInjectionManager
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // MEDIATR
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetUserByIdHandler).Assembly));

            // USER
            services.AddScoped<IUserService, UserService>();

            // UNIT OF WORK
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // BCrypt
            services.AddScoped<IBCryptService, BCryptService>();

            return services;
        }


        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            // USER
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }


        public static IServiceCollection AddPresentationAPILayer(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();

            // Problem Details format, more at https://www.rfc-editor.org/rfc/rfc9457
            services.AddProblemDetails();
            services.AddExceptionHandler<CustomExceptionHandler>();

            // Swagger/OpenAPI, more at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            return services;
        }
    }
}
