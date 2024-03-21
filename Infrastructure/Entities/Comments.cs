using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Comments
    {
        public Comments() => Date = DateTime.Now;

        public long Id { get; private set; }
        public required string Description { get; set; }

        public User User { get; set; } = null!;

        public ProjectTask ProjectTask { get; set; } = null!;
        public DateTime Date { get; private set; }
    }
}
