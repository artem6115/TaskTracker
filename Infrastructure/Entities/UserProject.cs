using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("Users_To_Projects")]
    public class UserProject
    {
        public long UserId { get; set; }
        public long ProjectId { get; set; }
        public User User { get; set; } = null!;
        public Project Project { get; set; } = null!;


    }
}
