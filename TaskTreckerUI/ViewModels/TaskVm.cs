using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Filters;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;

namespace TaskTrackerUI.ViewModels
{
    public class TaskVm : VMBase
    {
        public TaskFilter? Filter { get; set; }
        ObservableCollection<TaskDto> _tasks;
        public ObservableCollection<TaskDto> Tasks { get=>_tasks; set 
                {
                _tasks = value;
                _tasks.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => Refresh();
                Refresh();
            }
         }
        ObservableCollection<TaskDto> _tasksView;
        public ObservableCollection<TaskDto> TasksView
        {
            get => _tasksView; set
            {
                _tasksView = value;
                OnPropertyChanged();
            }
        }

        public void Refresh()
            => TasksView = (Filter is null) ? Tasks : Filter.UseFilter(Tasks); 

        public override async Task<bool> LoadData() {
            var list = await TaskService.GetMyTasksAsync();
            if (list is null) return false;
            Tasks = new ObservableCollection<TaskDto>(list);
            return true;
        }
    }
}
