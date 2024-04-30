﻿using Infrastructure.Entities;
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

        public async Task ChangeProjectTeam(IEnumerable<UserProject> AddUsers, IEnumerable<UserProject> RemoveUsers)
        {
           await _context.UsersProjects.AddRangeAsync(AddUsers);
           _context.UsersProjects.RemoveRange(RemoveUsers);
           await  _context.SaveChangesAsync();
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            var entity = await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Создан проект, наименоание {entity.Entity.Name}, id - {entity.Entity.Id}");
            return entity.Entity;
        }

        public async Task DeleteProjectAsync(long Id)
        {
            var entity = await _context.Projects.FindAsync(Id);
            if(entity == null)
                throw new FileNotFoundException("Проект не найден");
            if (entity.AuthorId != UserClaims.User.Id)
                throw new AccessViolationException("Вы не можете удалить чужой проект");
            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Удален проект, наименоание {entity.Name}, id - {entity.Id}");
        }

        public async Task<List<Project>> GetMyProjectsAsync()
        {
            var entities = await _context.Projects
                .Where(x=>x.AuthorId == UserClaims.User.Id)
                .AsNoTracking()
                .ToListAsync();
            return entities;
        }

        public async Task<List<Project>> GetParticipateProjectsAsync()
        {
            var ProjectsId = await _context.UsersProjects
                .Where(x => x.UserId == UserClaims.User.Id)
                .AsNoTracking()
                .Select(x=>x.ProjectId)
                .ToListAsync();
            var entities = await _context.Projects
                .Where(x =>ProjectsId.Contains(x.Id))
                .AsNoTracking()
                .ToListAsync();
            return entities;
        }

        public async Task<Project> GetProjectAsync(long Id)
        {
            var entity = await _context.Projects.FindAsync(Id);
            if (entity == null)
                throw new FileNotFoundException("Проект не найден");
            return entity;
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Project updated, id - {project.Id}, description - {project.Name}");
            return project;

        }
    }
}
