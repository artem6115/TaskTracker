using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth
{
    public class User
    {
        public long Id { get; private set; }
        public required string FullName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public int AccessFaildCount { get; set; }

        public bool Banned { get; set; }

        public bool Confirmed { get; set; }

        public bool Deleted { get; set; }

        public string? Phone { get; set; }
        public List<Note>? Notes { get; set; }
        public List<Project>? Projects { get; set; }
        public List<ProjectTask>? Tasks { get; set; }


    }
}
