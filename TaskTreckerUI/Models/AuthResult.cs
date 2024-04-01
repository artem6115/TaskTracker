using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTreckerUI.Models
{
    internal class AuthResult
    { 
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        
    }
}
