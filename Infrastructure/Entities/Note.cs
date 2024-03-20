using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Note
    {
        public required long Id { get; set; }
        public required string Description { get; set; }
        public DateTime? DateOfCreate { get; set; }
        public DateTime? DateOfEdit { get; set; }


    }
}
