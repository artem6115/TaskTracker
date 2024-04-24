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

        public ObservableCollection<TaskDto> UseFilter(IList<TaskDto> tasks) {
            var list = new List<TaskDto>();
            foreach (var task in tasks)
            {
                if (TaskStatus != Models.TaskStatus.All && TaskStatus != task.StatusTask) continue;
                list.Add(task);
            }
            return new ObservableCollection<TaskDto>(list);
        }
       
    }

}
