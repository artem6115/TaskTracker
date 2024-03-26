using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Epic
    {
        public long Id { get; private set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        public required long ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public List<WorkTask> Tasks { get; set; } = null!;

    }
}
