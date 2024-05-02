using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Models
{
    public class ProjectDetails
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public long AuthorId { get; set; }
        public UserClaims Author { get; set; } = null!;
        public List<UserClaims> Users { get; set; }
        public List<Epic> Epics { get; set; }
    }
}
