using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Epics
{
    public class GetProjectEpicsQuery : IRequest<List<EpicDto>>
    {
        public long Id { get; set; }
    }
}
