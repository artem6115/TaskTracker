using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Models
{
    public class TaskDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public Models.TaskStatus StatusTask { get; set; }
        public DateTime? ApproximateDateOfCompleted { get; set; }
        public DateTime? DateOfClosed { get; set; }
        public DateTime DateOfCreated { get; set; }
    }
    public enum TaskStatus
    {
        Free,
        Work,
        Pause,
        Blocked,
        Completed,
        All
    }
}
