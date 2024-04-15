using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Notes.Create
{
    public class NoteCreateCommand : IRequest<NoteDto>
    {
        public string Description { get; set; }
        public DateTime DateOfCreated { get; private set; }
        public DateTime? DateOfChanged { get; set; }
    }
}
