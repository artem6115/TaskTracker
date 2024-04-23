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
            var resultTask = await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Task added, id - {resultTask.Entity.Id}, description - {resultTask.Entity.Description}");
            return resultTask.Entity;
        }

        public async Task<bool> DeleteTaskAsync(long id)
        {
            var task = await _context.Tasks.AsNoTracking().SingleOrDefaultAsync(x=>x.Id == id);
            if (task is null)
                throw new FileNotFoundException("Task not found");
            await CheckAccess(task);
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
                throw new ArgumentException("Access denied");
            return task;
        }

        public async Task<List<WorkTask>> GetTasksForEpicAsync(long id)
        {
            return await _context.Tasks.AsNoTracking()
                .Where(task => task.EpicId == id).ToListAsync();
        }

        public async Task<WorkTask> UpdateTaskAsync(WorkTask task)
        {
            await CheckAccess(task);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Task updated, id - {task.Id}, description - {task.Description}");
            return task;
        }
        private async Task CheckAccess(WorkTask task)
        {
            if (task.EpicId == null)
            {
                if (task.UserId != UserClaims.User.Id)
                    throw new ArgumentException("Access denied");
            }
            else
            {
                var authResult = await _context.Epics.AsNoTracking().SingleOrDefaultAsync(x =>
                x.Project.AuthorId == UserClaims.User.Id &&
                x.Id == task.EpicId);
                if (authResult is null)
                    throw new ArgumentException("Access denied");
            }

        }
    }
}
