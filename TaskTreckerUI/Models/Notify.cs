using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Models
{
    public class Notify
    {
        public long Id { get;  set; }
        public string Message { get; set; }
        public bool WasRead { get; set; }

        public DateTime Date { get; set; }
    }
}
