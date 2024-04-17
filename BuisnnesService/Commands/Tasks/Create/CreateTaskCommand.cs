using Infrastructure.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Tasks.Create
{
    public class CreateTaskCommand : IRequest<TaskView>
    {
        public byte? Importance { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? ApproximateDateOfCompleted { get; set; }

        public long? PreviousTaskId { get; set; }

        public long? EpicId { get; set; }
        public long? UserId { get; set; }
    }
}
