using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Models
{
    public class Note
    {
        public long Id { get;  set; }
        public  string Description { get; set; }
        public DateTime DateOfCreated { get; set; }
        public DateTime? DateOfChanged { get; set; }
    }
}
