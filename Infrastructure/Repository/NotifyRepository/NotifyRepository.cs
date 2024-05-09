using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.NotifyRepository
{
    public class NotifyRepository : INotifyRepostitory
    {
        private readonly TaskTrackerDbContext _context;
        private readonly ILogger<NotifyRepository> _logger;
        public NotifyRepository(TaskTrackerDbContext context, ILogger<NotifyRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteAllNotifies(DateTime time)
        {
            var entities = await _context.Notifies
                .Where(x => x.UserId == UserClaims.User.Id)
                .Where(x => x.Date <= time)
                .ToListAsync();
            _context.Notifies.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotify(long Id)
        {
            var entity = await _context.Notifies.SingleOrDefaultAsync(x=>x.Id == Id);
            if (entity is null)
                throw new FileNotFoundException("Уведомление не найдено");
            if (entity.UserId != UserClaims.User.Id)
                throw new AccessViolationException("Вы не можете удалить данное уведомление");
            _context.Notifies.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Notify>> GetAllNotifies()
            => await _context.Notifies
                .AsNoTracking()
                .Where(x => x.UserId == UserClaims.User.Id)
                .ToListAsync();

        public async Task ReadAllNotifies(DateTime time)
        {
            var entities = await _context.Notifies
                .Where(x => x.UserId == UserClaims.User.Id)
                .Where(x => x.Date <= time && !x.WasRead)
                .ToListAsync();
            entities.ForEach(x=>x.WasRead = true);
            await _context.SaveChangesAsync();
        }

        public async Task<Notify> CreatelNotify(Notify notify)
        {
            var entity = await _context.Notifies.AddAsync(notify);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

    }
}
