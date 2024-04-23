using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Filters
{
    public class TaskFilter
    {
        public Models.TaskStatus TaskStatus { get; set; }
        public long ProjectId { get; set; }

        public static ObservableCollection<TaskView> GetViews(IList<TaskDto> tasks)
        {
            var list = new ObservableCollection<TaskView>();
            foreach(var task in tasks)
            {
                var view = new TaskView() {
                    Id=task.Id,
                    Title=task.Title,
                    StatusTask = task.StatusTask,
                    ApproximateDateOfCompleted = task.ApproximateDateOfCompleted,
                    Date = task.DateOfCreated
                };
                if(task.DateOfClosed != null)
                {
                    view.Date = (DateTime)task.DateOfClosed;
                    view.DatePropertyName = "Дата закрытия: ";
                }
                switch(view.StatusTask)
                {
                    case Models.TaskStatus.Free:
                        view.Background = Brushes.Orange; break;
                    case Models.TaskStatus.Work:
                        view.Background = Brushes.Blue; break;
                    case Models.TaskStatus.Pause:
                        view.Background = Brushes.Gray; break;
                    case Models.TaskStatus.Blocked:
                        view.Background = Brushes.Red; break;
                    case Models.TaskStatus.Completed:
                        view.Background = Brushes.Green; break;
                }
                list.Add(view);
            }
            return list;
        }

        public ObservableCollection<TaskView> UseFilter(IList<TaskDto> tasks) {
            var list = new List<TaskDto>();
            foreach (var task in tasks)
            {
                if (TaskStatus != Models.TaskStatus.All && TaskStatus != task.StatusTask) continue;
                list.Add(task);
            }
            return GetViews(list);
        }
       
    }

}
