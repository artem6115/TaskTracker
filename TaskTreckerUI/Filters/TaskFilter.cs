using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Filters
{
    public class TaskFilter
    {
        public Models.TaskStatus TaskStatus { get; set; }
        public bool LocalOnly = false;
        public long ProjectId { get; set; }
        public ProjectDto? Project { get; set; }

        public ObservableCollection<TaskDto> UseFilter(IList<TaskDto> tasks) {
            var list = new List<TaskDto>();
            foreach (var task in tasks)
            {
                if (TaskStatus != Models.TaskStatus.All 
                    && TaskStatus != task.StatusTask) continue;
                if (LocalOnly) 
                {  
                    if(task.Epic is not null) continue; 
                }
                else if (Project is not null)
                {
                    if (task.Epic is null || task.Epic.Project.Id != Project.Id) continue;
                }
                    
                list.Add(task);
            }
            return new ObservableCollection<TaskDto>(list.OrderByDescending(x=>x.ApproximateDateOfCompleted));
        }
       
    }

}
