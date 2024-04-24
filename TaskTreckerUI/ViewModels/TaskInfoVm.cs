using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;
namespace TaskTrackerUI.ViewModels
{
    public class TaskInfoVm : VMBase
    {
        TaskInfo _task;
        public long? TaskId { get; set; }
        public TaskInfo Task { get=>_task; set { _task = value;OnPropertyChanged(); } }
        public async override Task<bool> LoadData()
        {
            if(TaskId is null) return false;
            var resultTask = await TaskService.GetTaskAsync((long)TaskId);
            if(resultTask is not null) Task=resultTask;
            return resultTask != null;
        }
    }
}
