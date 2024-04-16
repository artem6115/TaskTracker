using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Models
{
    public class Note
    {
        public long Id { get;  set; }
        public  string Description { get; set; }
        public DateTime DateOfCreated { get; set; }
        public DateTime? DateOfChanged { get; set; }

        public void WrappDescription()
        {
            var changs = Description.Chunk(150).ToArray();
            string[] lines = new string[changs.Length];
            for (int i = 0; i < changs.Length; i++)
            {
                lines[i] = string.Join(null, changs[i]);
            }
            var result = string.Join(Environment.NewLine, lines);
            Description = result;
        }
    }
}
