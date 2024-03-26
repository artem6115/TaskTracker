using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Notify
    {
        public int Id { get;private set; }
        public required string Title { get; set; }
        public required string Message { get; set; }
        public DateTime Date { get; set; }
        public required long UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
