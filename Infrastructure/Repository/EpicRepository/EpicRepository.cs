using Infrastructure.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.EpicRepository
{
    public class EpicRepository : IEpicRepository
    {
        private readonly TaskTrackerDbContext _context;
        private readonly ILogger<EpicRepository> _logger;
        public EpicRepository(TaskTrackerDbContext context, ILogger<EpicRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Epic> CreateEpicAsync(Epic epic)
        {
            var newEpic = await _context.Epics.AddAsync(epic);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New Epic {newEpic.Entity.Id}, {newEpic.Entity.Title}");
            return newEpic.Entity;

        }

        public async Task DeleteEpicAsync(long Id)
        {
            var entity = await _context.Epics
                .Include(x=>x.Project)
                .SingleOrDefaultAsync(x => x.Id == Id);
            if (entity is null)
                throw new FileNotFoundException("Эпик не найден");
            if (entity.Project.AuthorId != UserClaims.User.Id)
                throw new AccessViolationException("Удаление эпика запрещено");
            _context.Epics.Remove(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Delete Epic {Id}");
        }

        public async Task<Epic> GetEpicAsync(long Id)
        {
            var entity = await _context.Epics
                .Include(e=>e.Project)
                .SingleOrDefaultAsync(x => x.Id == Id);
            if (entity is null)
                throw new FileNotFoundException("Эпик не найден");
            return entity;
        }

        public async Task<List<Epic>> GetProjectEpicsAsync(long ProjectId)
        {
            var entities = await _context.Epics
               .Where(x => x.ProjectId == ProjectId)
               .AsNoTracking()
               .ToListAsync();
            return entities;
        }

        public async Task<Epic> UpdateEpicAsync(Epic epic)
        {
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Update Epic {epic.Id}, {epic.Title}");
            return epic;
        }
    }
}
