using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Attachment
    {
        public long Id { get; private set; }
        public byte[] Data { get; set; } = null!;
        public AttachmentTypes Type { get; set; }
        public required string Extention { get; set; }

        public required User User { get; set; } = null!;
        public WorkTask WorkTask { get; set; } = null!;
    }
}
