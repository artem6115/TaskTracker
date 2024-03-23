using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Attachments
    {
        public long Id { get; private set; }
        public byte[] Data { get; set; } = null!;
        public AttachmentTypes Type { get; set; }
        public required string Extention { get; set; }

        public required long UserId { get; set; }
        public User User { get; set; } = null!;

        public required long TaskId { get; set; }
        public ProjectTask ProjectTask { get; set; } = null!;
    }
}
