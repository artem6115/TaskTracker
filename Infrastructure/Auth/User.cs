using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth
{
    public class User
    {
        public required long Id { get; set; }
        public required string Name { get; set; }

        public required string Surname { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public int AccessFaildCount { get; set; }

        public bool UserBanned { get; set; }

        public bool UserConfirmed { get; set; }

        public bool UserDeleted { get; set; }

        public string? Phone { get; set; }


    }
}
