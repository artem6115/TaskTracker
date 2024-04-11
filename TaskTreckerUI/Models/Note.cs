using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTreckerUI.Models
{
    public class Note
    {
        public Note() => DateOfCreated = DateTime.Now;
        public long Id { get; private set; }
        public required string Description { get; set; }
        public DateTime DateOfCreated { get; private set; }
        public DateTime? DateOfChanged { get; set; }
        public required User User { get; set; } = null!;
    }
}
