using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.NotifyRepository
{
    public class NotifyProcedureRepository : INotifyRepostitory
    {
        private readonly TaskTrackerDbContext _context;
        private readonly ILogger<NotifyProcedureRepository> _logger;
        public NotifyProcedureRepository(TaskTrackerDbContext context, ILogger<NotifyProcedureRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteAllNotifies(DateTime time)
            => _context.DeleteAll_Notifies(time);

        public async Task DeleteNotify(long Id)
        {
            var entity = await _context.Notifies
                .AsNoTracking()
                .SingleOrDefaultAsync(x=>x.Id == Id);
            if (entity is null)
                throw new FileNotFoundException("Уведомление не найдено");
            if (entity.UserId != UserClaims.User.Id)
                throw new AccessViolationException("Вы не можете удалить данное уведомление");
            await _context.Delete_Notify(Id);
        }

        public async Task<List<Notify>> GetAllNotifies()
            => await _context.Notifies
                .AsNoTracking()
                .Where(x => x.UserId == UserClaims.User.Id)
                .ToListAsync();

        public async Task ReadAllNotifies(DateTime time)
            => _context.ReadAll_Notifies(time);
        public async Task<Notify> CreatelNotify(Notify notify)
        {
            var Id = await _context.Create_Notify(notify);
            return await _context.Notifies.SingleAsync(x=>x.Id == Id);
        }
    }
}
