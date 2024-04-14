using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Models
{
    public class User
    {
        public  string FullName { get; set; }
        public  string Email { get; set; }
        public  string Phone { get; set; }
        public string Token { get; set; }
        public string RefrashToken { get; set; }
        public long Id { get;  set; }

        public string Password { get; set; }
        public string Spice { get; set; }

        public int AccessFaildCount { get; set; }
        public bool Banned { get; set; }
        public bool Confirmed { get; set; }
        public bool Deleted { get; set; }



    }
}
