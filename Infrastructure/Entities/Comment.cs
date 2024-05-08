using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Comment
    {
        public long Id { get; private set; }
        public required string Description { get; set; }
        public User User { get; set; } = null!;
        public long UserId { get; set; }

        public required long WorkTaskId { get; set; }
        public DateTime Date { get; private set; }
    }
}
