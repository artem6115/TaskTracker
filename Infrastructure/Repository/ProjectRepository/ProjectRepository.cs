using Infrastructure.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.ProjectRepository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ILogger<ProjectRepository> _logger;
        private readonly TaskTrackerDbContext _context;
        public ProjectRepository(TaskTrackerDbContext context, ILogger<ProjectRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task ChangeProjectTeam(long projectId, List<long> usersId)
            {
            var project = await GetProjectAsync(projectId);
            if(usersId is null) return;
            var entities = await _context.UsersProjects
               .AsNoTracking()
               .Where(x => x.ProjectId == projectId)
               .ToListAsync();
            var entityToDelete = entities.ExceptBy(usersId, x => x.UserId).ToList();
            var entityToAdd = usersId.Except(entities
                .Select(x => x.UserId))
                .Select(x => new UserProject
                { UserId = x, ProjectId = projectId }
                ).ToList();
            if (entityToAdd.Count > 0)
            {
                await _context.UsersProjects.AddRangeAsync(entityToAdd);
                var notifies = entityToAdd.Select(x => new Notify() {
                    Message = $"Вы были добавлены в проект {project.Name}",
                    UserId = x.UserId
                    });
                await _context.Notifies.AddRangeAsync(notifies);
            }
            if (entityToDelete.Count > 0) { 
                _context.UsersProjects.RemoveRange(entityToDelete);
                var notifies = entityToDelete.Select(x => new Notify()
                {
                    Message = $"Вы были удалены из проекта {project.Name}",
                    UserId = x.UserId
                });
                await _context.Notifies.AddRangeAsync(notifies);
            }

            await _context.SaveChangesAsync();

        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            var entity = await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Создан проект, наименоание {entity.Entity.Name}, id - {entity.Entity.Id}");
            await entity.Navigation("Author").LoadAsync();
            return entity.Entity;
        }

        public async Task DeleteProjectAsync(long Id)
        {
            var entity = await _context.Projects.SingleAsync(x => x.Id == Id);
            if (entity == null)
                throw new FileNotFoundException("Проект не найден");
            if (entity.AuthorId != UserClaims.User.Id)
                throw new AccessViolationException("Вы не можете удалить чужой проект");
            _context.Projects.Remove(entity);
            var users = await GetUsers(Id);
            var notifies = users.Select(x=>new Notify() { 
                Message=$"Проект {entity.Name} был удален",
                UserId = x.Id
                });
            await _context.Notifies.AddRangeAsync(notifies);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Удален проект, наименоание {entity.Name}, id - {entity.Id}");
        }

        public async Task<List<Project>> GetMyProjectsAsync()
        {
            var entities = await _context.Projects
                .Where(x => x.AuthorId == UserClaims.User.Id)
                .AsNoTracking()
                .Include(x => x.Author)
                .ToListAsync();
            return entities;
        }

        public async Task<List<Project>> GetParticipateProjectsAsync()
        {
            var ProjectsId = await _context.UsersProjects
                .Where(x => x.UserId == UserClaims.User.Id)
                .AsNoTracking()
                .Select(x => x.ProjectId)
                .ToListAsync();
            var entities = await _context.Projects
                .Where(x => ProjectsId.Contains(x.Id))
                .AsNoTracking()
                .Include(x => x.Author)
                .ToListAsync();
            return entities;
        }

        public async Task<Project> GetProjectAsync(long Id)
        {
            
            var entity = await _context.Projects
                .Include(x => x.Author)
                .Include(x => x.Epics)
                .SingleOrDefaultAsync(x => x.Id == Id);
            if (entity == null)
                throw new FileNotFoundException("Проект не найден");
            return entity;
        }

        public async Task<List<User>> GetUsers(long projectId)
            => await _context.UsersProjects
            .AsNoTracking()
            .Where(x => x.ProjectId == projectId)
            .Select(x => x.User)
            .ToListAsync();
        

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            var users = await GetUsers(project.Id);
            var notifies = users.Select(x => new Notify()
            {
                Message = $"Проект {project.Name} был изменен",
                UserId = x.Id
            });
            await _context.Notifies.AddRangeAsync(notifies);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Project updated, id - {project.Id}, description - {project.Name}");
            var projectNew = await GetProjectAsync(project.Id);
            return projectNew;

        }
    }
}
