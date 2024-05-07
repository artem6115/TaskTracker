using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Models
{
    public class TaskInfo
    {
        public long Id { get; set; }
        public byte? Importance { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DateOfCreated { get; set; }
        public DateTime? DateOfClosed { get; set; }
        public DateTime? ApproximateDateOfCompleted { get; set; }

        public Models.TaskStatus StatusTask { get; set; }

        public TaskDto? PreviousTask { get; set; }

        public long? EpicId { get; set; }
        public Epic? Epic { get; set; }


        public User? User { get; set; }
    }
}
