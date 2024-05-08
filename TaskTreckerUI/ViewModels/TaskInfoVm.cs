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
        List<TaskDto> _tasks;

        public long? TaskId { get; set; }
        public long? EpicId { get; set; }

        public TaskInfo Task { get=>_task; set { _task = value;OnPropertyChanged(); } }

        public List<TaskDto> TasksInScope { get => _tasks; set { _tasks = value;OnPropertyChanged(); } }

        public async override Task<bool> LoadData()
        {
            if(TaskId is null) return false;
            var resultTask = await TaskService.GetTaskAsync((long)TaskId);
            if(resultTask is not null) Task=resultTask;
            //List<TaskDto> anotherTasks;
            //if (EpicId is null) 
            //    anotherTasks = await TaskService.GetMyTasksAsync();
            //else
            //    anotherTasks = await TaskService.GetTasksInEpic((long)EpicId);
            //if(anotherTasks is not null)anotherTasks.RemoveAll(x => x.Id == WorkTaskId);
            //TasksInScope = anotherTasks;
            return resultTask != null;
        }
    }
}
