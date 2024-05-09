using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Notify
    {
        public Notify() => Date = DateTime.Now;
        public int Id { get;private set; }
        public bool WasRead { get; set; } = false;
        public required string Message { get; set; }
        public DateTime Date { get; set; }
        public required long UserId { get; set; }
    }
}
