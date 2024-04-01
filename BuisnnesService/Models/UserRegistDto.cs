using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Models
{
    public class UserRegistDto : UserLoginDto
    {
        public string FullName { get; set; } = null;
        public string? Phone { get; set; }

    }
}
