using DomainLayer.partonair.Contracts;

using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.Services;
using ApplicationLayer.partonair.MediatR.Configurations;

using InfrastructureLayer.partonair.Repositories;

using API.partonair.MiddlewareCustomExceptions;


namespace API.partonair.GlobalManager
{
    public static class DependencyInjectionManager
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // MEDIATR
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(ApplicationLayerMediatRConfiguration).Assembly);
            });

            // USER
            services.AddScoped<IUserService, UserService>();

            // PROFILE
            services.AddScoped<IProfileService, ProfileService>();

            // CONTACT
            services.AddScoped<IContactService, ContactService>();

            // EVALUATION
            services.AddScoped<IEvaluationService, EvaluationService>();

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

            //PROFILE
            services.AddScoped<IProfileRepository, ProfileRepository>();

            // CONTACT
            services.AddScoped<IContactRepository, ContactRepository>();

            // EVALUATION
            services.AddScoped<IEvaluationRepository, EvaluationRepository>();

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
