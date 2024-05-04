using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Models
{
    public class Epic
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectDto Project { get; set; }
    }
}
