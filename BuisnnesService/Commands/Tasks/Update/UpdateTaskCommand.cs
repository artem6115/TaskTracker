using Infrastructure.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Tasks.Update
{
    public class UpdateTaskCommand : IRequest<TaskView>
    {
        public long Id { get; set; }
        public byte? Importance { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }

        public DateTime? DateOfClosed { get; set; }
        public DateTime? ApproximateDateOfCompleted { get; set; }

        public Infrastructure.Entities.TaskStatus? StatusTask { get; set; }

        public long? PreviousTaskId { get; set; }
        public long? UserId { get; set; }
    }
}
