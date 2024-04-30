using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.TaskRepository
{
    public class TaskProcedureRepository : ITaskRepository
    {
        private readonly ILogger<TaskProcedureRepository> _logger;
        private readonly TaskTrackerDbContext _context;
        public TaskProcedureRepository(TaskTrackerDbContext context, ILogger<TaskProcedureRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<WorkTask> CreateTaskAsync(WorkTask task)
        {
            await _context.Create_Task(task);
            var newTask = await _context.Tasks.Where(x=>x.UserId == task.UserId).OrderByDescending(x=>x.DateOfCreated).FirstAsync();
            _logger.LogDebug($"Task added, id - {newTask.Id}, description - {newTask.Description}");
            return newTask;
        }

        public async Task DeleteTaskAsync(long id)
        {
            var task = await _context.Tasks.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (task is null)
                throw new FileNotFoundException("Task not found");
            await CheckAccess(task);
            var nextTask = await _context.Tasks.AsNoTracking().SingleOrDefaultAsync(x => x.PreviousTaskId == id);
            if (nextTask is not null) {
                nextTask.PreviousTaskId = null; 
                await _context.Update_Task(nextTask);
             }
            await _context.Delete_Task(id);
            _logger.LogDebug($"Task deleted, id - {task.Id}, description - {task.Description}");
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
                .Include(task => task.PreviousTask)
                .Include(task=>task.User)
                .SingleOrDefaultAsync(task => task.Id == id);
            if (task is null)
                throw new FileNotFoundException("Task not found");
            if (task.EpicId == null && task.UserId != UserClaims.User.Id)
                throw new AccessViolationException("Do not have access to this resource");
            return task;
        }

        public async Task<List<WorkTask>> GetTasksForEpicAsync(long id)
        {
            return await _context.Tasks.AsNoTracking()
                .Where(task => task.EpicId == id).ToListAsync();
        }

        public Task UnclockTasksAsync(long id)
        {
            return null!;
        }
        public Task LockTasksAsync(long id)
        {
            return null!;
        }

        public async Task<WorkTask> UpdateTaskAsync(WorkTask task)
        {
            await CheckAccess(task);
            await _context.Update_Task(task);
            var resultTask = await _context.Tasks.SingleAsync(x=>x.Id==task.Id);
            _logger.LogDebug($"Task updated, id - {resultTask.Id}, description - {resultTask.Description}");
            return resultTask;
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
                var authResult = await _context.Epics.SingleOrDefaultAsync(x =>
                x.Project.AuthorId == UserClaims.User.Id &&
                x.Id == task.EpicId);
                if (authResult is null)
                    throw new AccessViolationException("Do not have access to this resource");
            }

        }
    }
}
