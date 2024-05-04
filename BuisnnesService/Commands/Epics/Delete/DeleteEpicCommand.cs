using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Epics.Delete
{
    public class DeleteEpicCommand : IRequest
    {
        public long Id { get; set; }
    }
}
