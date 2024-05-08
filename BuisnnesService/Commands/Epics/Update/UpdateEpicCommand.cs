using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Epics.Update
{
    public class UpdateEpicCommand : IRequest<EpicDto>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
