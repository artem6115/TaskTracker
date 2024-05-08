using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public long WorkTaskId { get; set; }
        public DateTime Date { get;  set; }
        public bool IsMyComment { get; set; } = false;
    }
}
