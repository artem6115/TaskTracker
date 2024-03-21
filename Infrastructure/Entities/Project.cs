using Infrastructure.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Project
    {
        public long Id { get; private set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public required long AuthorId { get; set; }
        public User Author { get; set; } = null!;
        public List<User> Users { get; set; } = null!;
        public List<Epic> Epics { get; set; } = null!;


    }
}
