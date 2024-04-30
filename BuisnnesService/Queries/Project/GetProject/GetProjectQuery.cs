using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Project.GetProject
{
    public class GetProjectQuery : IRequest<Infrastructure.Entities.Project>
    {
        public long Id { get; set; }
    }
}
