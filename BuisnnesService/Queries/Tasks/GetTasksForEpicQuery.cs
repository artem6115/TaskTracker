using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Tasks
{
    public class GetTasksForEpicQuery : IRequest<List<TaskView>>
    {
        public long Id { get; set; }
    }
}
