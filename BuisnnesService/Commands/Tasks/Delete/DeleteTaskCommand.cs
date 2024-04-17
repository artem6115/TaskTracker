using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Tasks.Delete
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}
