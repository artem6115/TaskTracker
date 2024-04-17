using Infrastructure.Auth;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BuisnnesService.Models
{
    public class TaskDto
    {
        public long Id { get;  set; }
        public byte? Importance { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DateOfCreated { get;  set; }
        public DateTime? DateOfClosed { get; set; }
        public DateTime? ApproximateDateOfCompleted { get; set; }

        public Infrastructure.Entities.TaskStatus StatusTask { get; set; }

        public TaskView? PreviousTask { get; set; }

        public EpicView? Epic { get; set; }
        public UserClaims? User { get; set; }
    }
}
