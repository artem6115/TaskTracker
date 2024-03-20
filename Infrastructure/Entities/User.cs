using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth
{
    public class User : ApplicationUser
    {
        public required long Id { get; set; }

        public HashSet<Note>? Notes { get; set; }
        public HashSet<Project>? Projects { get; set; }
        public HashSet<ProjectTask>? Tasks { get; set; }


    }
}
