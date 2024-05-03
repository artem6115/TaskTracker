using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Models
{
    public class ChangeProjectTeamQuery
    {
        public long ProjectId { get; set; }
        public IEnumerable<long> UsersId { get; set; }

    }
}
