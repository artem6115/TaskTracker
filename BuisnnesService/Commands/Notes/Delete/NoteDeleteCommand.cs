using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Notes.Delete
{
    public class NoteDeleteCommand : IRequest
    {
        public long Id { get; set; }
    }
}
