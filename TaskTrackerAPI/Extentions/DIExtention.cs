using AutoMapper;
using BuisnnesService.Services;
using Infrastructure.EntitiesConfigurations;
using Infrastructure.Repository.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Repository.NoteRepository;

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
            AddSingleton<SpiceGenerator>().
            AddTransient<UserManagerService>();

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<INoteRepository, NoteRepository>();

            return services;
        }

        public static IServiceCollection AddStoredProceduresRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserProcedureRepository>();
            services.AddTransient<INoteRepository, NoteProcedureRepository>();

            return services;
        }

    }
}
