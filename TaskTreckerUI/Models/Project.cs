using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public long AuthorId { get; set; }
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();


    }
}
