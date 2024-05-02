using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Project.GetProject
{
    public class GetProjectQuery : IRequest<ProjectDetails>
    {
        public long Id { get; set; }
    }
}
