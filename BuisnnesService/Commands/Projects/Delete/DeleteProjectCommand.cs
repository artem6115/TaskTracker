using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Projects.Delete
{
    public class DeleteProjectCommand : IRequest
    {
        public long Id { get; set; }
    }
}
