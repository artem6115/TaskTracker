using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Tasks.Update
{
    public class UpdateStatusTaskCommand : IRequest<TaskView>
    {
        public long Id { get; set; }
        public Infrastructure.Entities.TaskStatus StatusTask { get; set; }
    }
}
