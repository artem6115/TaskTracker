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
            if (task.EpicId is not null)
            {
                var result = await _context.Epics
                    .AsNoTracking()
                    .SingleOrDefaultAsync(
                    x => x.Id == task.EpicId &&
                    x.Project.AuthorId == UserClaims.User.Id);
                if (result is null)
                    throw new FileNotFoundException("Эпик не найден");
            }
            await _context.Create_Task(task);
            var newTask = await _context.Tasks.Where(x=>x.UserId == task.UserId).OrderByDescending(x=>x.DateOfCreated).FirstAsync();
            _logger.LogDebug($"Task added, id - {newTask.Id}, description - {newTask.Description}");
            return newTask;
        }

        public async Task DeleteTaskAsync(long id)
        {
            var task = await _context.Tasks
                            .AsNoTracking()
                            .Include(x => x.Epic).ThenInclude(x => x.Project)
                            .SingleOrDefaultAsync(x => x.Id == id);
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
                .Include(x => x.Epic)
                .ThenInclude(x => x.Project)
                .Where(task => task.UserId == UserClaims.User.Id)
                .ToListAsync();

        }

        public async Task<WorkTask> GetTaskAsync(long id)
        {
            var task = await _context.Tasks
                .Include(task => task.Epic)
                .ThenInclude(x => x.Project)
                .Include(task => task.PreviousTask)
                .Include(task=>task.User)
                .SingleOrDefaultAsync(task => task.Id == id);
            if (task is null)
                throw new FileNotFoundException("Task not found");
            await CheckAccess(task);
            return task;
        }

        public async Task<List<WorkTask>> GetTasksForEpicAsync(long id)
        {
            return await _context.Tasks.AsNoTracking()
                .Where(task => task.EpicId == id).ToListAsync();
        }

        public async Task UnclockTasksAsync(long id)
        {
            await Task.Yield();
        }
        public async Task LockTasksAsync(long id)
        {
            await Task.Yield();

        }

        public async Task<WorkTask> UpdateTaskAsync(WorkTask task)
        {
            await CheckAccess(task);
            await _context.Update_Task(task);
            var resultTask = await _context.Tasks.SingleAsync(x=>x.Id==task.Id);
            _logger.LogDebug($"Task updated, id - {resultTask.Id}, description - {resultTask.Description}");
            return resultTask;
        }
        public async Task<WorkTask> UpdateStatusTaskAsync(WorkTask task)
        {
            if (task.UserId != UserClaims.User.Id)
                throw new AccessViolationException("У вас нет прав на изменеие статуса задачи");
            await _context.Update_Task(task);
            _logger.LogDebug($"Task updated status, id - {task.Id}, description - {task.Description}");
            return await _context.Tasks.SingleAsync(x => x.Id == task.Id);
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
