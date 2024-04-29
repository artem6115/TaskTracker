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

        public async Task<bool> DeleteTaskAsync(long id)
        {
            var task = await _context.Tasks.AsNoTracking().SingleOrDefaultAsync(x=>x.Id == id);
            if (task is null)
                throw new FileNotFoundException("Task not found");
            await CheckAccess(task);
            var nextTask = await _context.Tasks.SingleOrDefaultAsync(x => x.PreviousTaskId == id);
            if (nextTask is not null) nextTask.PreviousTaskId = null;
            _context.Remove(task);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Task deleted, id - {task.Id}, description - {task.Description}");
            return true;

        }

        public async Task<List<WorkTask>> GetMyTasksAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(task => task.UserId == UserClaims.User.Id)
                .ToListAsync();

        }

        public async Task<WorkTask> GetTaskAsync(long id)
        {
            var task = await _context.Tasks
                .Include(task => task.Epic)
                .Include(task=>task.User)
                .Include(task => task.PreviousTask)
                .SingleOrDefaultAsync(task => task.Id == id);
            if (task is null)
                throw new FileNotFoundException("Task not found");
            if (task.EpicId == null && task.UserId != UserClaims.User.Id )
                throw new AccessViolationException("Do not have access to this resource");
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
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Task updated, id - {task.Id}, description - {task.Description}");
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
                var authResult = await _context.Epics.AsNoTracking().SingleOrDefaultAsync(x =>
                x.Project.AuthorId == UserClaims.User.Id &&
                x.Id == task.EpicId);
                if (authResult is null)
                    throw new AccessViolationException("Do not have access to this resource");
            }

        }
    }
}
