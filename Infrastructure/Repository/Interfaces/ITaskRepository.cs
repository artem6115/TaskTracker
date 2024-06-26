﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface ITaskRepository
    {
        public Task<WorkTask> GetTaskAsync(long id);
        public Task<List<WorkTask>> GetMyTasksAsync();
        public Task<List<WorkTask>> GetTasksForEpicAsync(long id);
        public Task<WorkTask> CreateTaskAsync(WorkTask task);
        public Task<WorkTask> UpdateTaskAsync(WorkTask task);
        public Task<WorkTask> UpdateStatusTaskAsync(WorkTask task);

        public Task DeleteTaskAsync(long id);
        public Task UnclockTasksAsync(long id);
        public Task LockTasksAsync(long id);

    }
}
