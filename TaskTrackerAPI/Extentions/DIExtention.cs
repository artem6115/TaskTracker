using AutoMapper;
using BuisnnesService.Services;
using Infrastructure.Repository.Interfaces;
using Infrastructure.Repository.Repositories;

namespace TaskTrackerAPI.DIExtentions
{
    public static class DIExtention
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(JwtAutorizationService).Assembly,
                typeof(Infrastructure.Utilits.TaskTrackerDbContext).Assembly,
                typeof(Program).Assembly)
                .AddScoped<Mapper>();
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<JwtAutorizationService>().
            AddTransient<UserManagerService>();

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddStoredProceduresRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserProcedureRepository>();
            return services;
        }

    }
}
