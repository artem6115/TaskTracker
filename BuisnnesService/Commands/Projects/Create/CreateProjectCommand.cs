using BuisnnesService.Commands.Projects.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Projects.Create
{
    public class CreateProjectCommand : IRequest<ProjectDto>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public long AuthorId { get; set; }
    }
}
