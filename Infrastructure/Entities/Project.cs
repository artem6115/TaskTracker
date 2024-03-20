using Infrastructure.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Project
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required User Author { get; set; }
        public HashSet<User> Users { get; set; } = null!;
        public HashSet<ProjectTask> ProjectTasks { get; set; } = null!;

    }
}
