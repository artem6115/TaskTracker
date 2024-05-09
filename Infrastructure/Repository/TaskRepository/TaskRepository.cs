using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.TaskRepository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ILogger<TaskRepository> _logger;
        private readonly TaskTrackerDbContext _context;
        public TaskRepository(TaskTrackerDbContext context,ILogger<TaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<WorkTask> CreateTaskAsync(WorkTask task)
        {
            if (task.EpicId is not null) {
                var result= await _context.Epics
                    .AsNoTracking()
                    .SingleOrDefaultAsync(
                    x=>x.Id == task.EpicId &&
                    x.Project.AuthorId == UserClaims.User.Id);
                if (result is null)
                    throw new FileNotFoundException("Эпик не найден");
            }
            if(await IsLockedTask(task))task.StatusTask = Entities.TaskStatus.Blocked;
            var resultTask = await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Task added, id - {resultTask.Entity.Id}, description - {resultTask.Entity.Description}");
            return resultTask.Entity;
        }

        private async Task<bool> IsLockedTask(WorkTask task)
        {
            if (task.PreviousTaskId is null) return false;
            var backEntity = await _context.Tasks.SingleAsync(x => x.Id == task.PreviousTaskId);
            return backEntity.StatusTask != Entities.TaskStatus.Completed;
        }

        public async Task DeleteTaskAsync(long id)
        {
            var task = await _context.Tasks
                           .AsNoTracking()
                           .Include(x => x.Epic).ThenInclude(x => x.Project)
                           .SingleOrDefaultAsync(x => x.Id == id);
            if (task is null)
                throw new FileNotFoundException("Task not found");
            if (task.UserId is not null)
                await _context.Notifies.AddAsync(new Notify()
                {
                    Message = $"Задача: {task.Title}, была удалена",
                    UserId = (long)task.UserId
                });
            await CheckAccess(task);
            await UnclockTasksAsync(id);
            _context.Remove(task);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Task deleted, id - {task.Id}, description - {task.Description}");

        }

        public async Task<List<WorkTask>> GetMyTasksAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .Include(x=>x.Epic)
                .ThenInclude(x=>x.Project)
                .Where(task => task.UserId == UserClaims.User.Id)
                .ToListAsync();

        }

        public async Task<WorkTask> GetTaskAsync(long id)
        {
            var task = await _context.Tasks
                .Include(task => task.Epic)
                .ThenInclude(x => x.Project)
                .Include(task=>task.User)
                .Include(task => task.PreviousTask)
                .SingleOrDefaultAsync(task => task.Id == id);
            if (task is null)
                throw new FileNotFoundException("Task not found");
           
            return task;
        }

        public async Task<List<WorkTask>> GetTasksForEpicAsync(long id)
        {
            return await _context.Tasks.AsNoTracking()
                .Where(task => task.EpicId == id).ToListAsync();
        }

        public async Task UnclockTasksAsync(long Id)
        {
            var entitiesToUnlock = await _context.Tasks.Where(x=>x.PreviousTaskId == Id).ToListAsync();
            foreach (var entity in entitiesToUnlock)
                entity.StatusTask = (entity.UserId is null) ? Entities.TaskStatus.Free : Entities.TaskStatus.Work;
            await _context.SaveChangesAsync();
        }
        public async Task LockTasksAsync(long Id)
        {
            var entitiesToUnlock = await _context.Tasks.Where(x => x.PreviousTaskId == Id).ToListAsync();
            foreach (var entity in entitiesToUnlock)
                entity.StatusTask =  Entities.TaskStatus.Blocked;
            await _context.SaveChangesAsync();
        }

        public async Task<WorkTask> UpdateTaskAsync(WorkTask task)
        {
            await CheckAccess(task);
            if (await IsLockedTask(task)) task.StatusTask = Entities.TaskStatus.Blocked;
            if (task.UserId is not null) { 
                var notify = new Notify()
                {
                    Message = $"Задача: {task.Title}, была изменена",
                    UserId = (long)task.UserId
                };
                await _context.Notifies.AddAsync(notify);
            }
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Task updated, id - {task.Id}, description - {task.Description}");
            return task;
        }
        public async Task<WorkTask> UpdateStatusTaskAsync(WorkTask task)
        {
            if (task.UserId != UserClaims.User.Id)
                throw new AccessViolationException("У вас нет прав на изменеие статуса задачи");
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Task updated status, id - {task.Id}, description - {task.Description}");
            return task;
        }
        private async Task CheckAccess(WorkTask task)
        {
            if (task.EpicId == null)
            {
                if (task.UserId != UserClaims.User.Id)
                    throw new AccessViolationException("Do not have access to this resource");
            }
            else
            {
                if (task.Epic?.Project is not null)
                {
                    if (task.Epic.Project.AuthorId != UserClaims.User.Id)
                        throw new AccessViolationException("Do not have access to this resource");
                }
            }

        }
    }
}
