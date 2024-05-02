using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public User Author { get; set; }
        public List<User> Users { get; set; }
        //public List<Epics> Epics { get; set; }

    }
}
