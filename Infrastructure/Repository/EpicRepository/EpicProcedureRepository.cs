using Infrastructure.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.EpicRepository
{
    public class EpicProcedureRepository : IEpicRepository
    {
        private readonly TaskTrackerDbContext _context;
        private readonly ILogger<EpicProcedureRepository> _logger;
        public EpicProcedureRepository(TaskTrackerDbContext context, ILogger<EpicProcedureRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Epic> CreateEpicAsync(Epic epic)
        {
            var proj = await _context.Projects
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == epic.ProjectId);
            if (proj == null)
                throw new FileNotFoundException("Проект с указаным id не найдет");
            if (proj.AuthorId != UserClaims.User.Id)
                throw new AccessViolationException("У вас нет прав на редактирование этого проекта");
            var Id = await _context.Create_Epic(epic);
            _logger.LogInformation($"Creare Epic {epic.Id}, {epic.Title}");
            var newEpic = await _context.Epics
                .AsNoTracking()
                .SingleAsync(x=>x.Id == Id);

            return newEpic;

        }

        public async Task DeleteEpicAsync(long Id)
        {
            await _context.Delete_Epic(Id);
            _logger.LogInformation($"Delete Epic {Id}");
        }

        public async Task<Epic> GetEpicAsync(long Id)
        {
            var entity = await _context.Epics
                .Include(e => e.Project)
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
            await _context.Update_Epic(epic);
            _logger.LogInformation($"Update Epic {epic.Id}, {epic.Title}");
            return epic;
        }
    }
}
