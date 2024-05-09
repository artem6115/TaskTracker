using AutoMapper;
using BuisnnesService.Services;
using Infrastructure.EntitiesConfigurations;
using Infrastructure.Repository.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Repository.NoteRepository;
using Infrastructure.Repository.TaskRepository;
using Infrastructure.Repository.ProjectRepository;
using Infrastructure.Repository.EpicRepository;
using Infrastructure.Repository.CommentRepository;
using Infrastructure.Repository.NotifyRepository;

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
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IEpicRepository, EpicRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<INotifyRepostitory, NotifyRepository>();



            return services;
        }

        public static IServiceCollection AddStoredProceduresRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserProcedureRepository>();
            services.AddTransient<INoteRepository, NoteProcedureRepository>();
            services.AddTransient<ITaskRepository, TaskProcedureRepository>();
            services.AddTransient<IProjectRepository, ProjectProcedureRepository>();
            services.AddTransient<IEpicRepository, EpicProcedureRepository>();
            services.AddTransient<ICommentRepository, CommentProcedureRepository>();
            services.AddTransient<INotifyRepostitory, NotifyProcedureRepository>();



            return services;
        }

    }
}
