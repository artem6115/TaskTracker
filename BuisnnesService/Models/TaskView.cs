using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Models
{
    public class TaskView
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public Infrastructure.Entities.TaskStatus StatusTask { get; set; }
        public DateTime? ApproximateDateOfCompleted { get; set; }
        public DateTime? DateOfClosed { get; set; }
        public DateTime DateOfCreated { get; set; }
    }
}
