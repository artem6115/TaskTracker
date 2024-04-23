using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Models
{
    public class NoteDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public DateTime DateOfCreated { get;  set; }
        public DateTime? DateOfChanged { get; set; }

    }
}
