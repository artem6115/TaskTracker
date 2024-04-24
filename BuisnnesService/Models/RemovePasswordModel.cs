using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Models
{
    public class RemovePasswordModel
    {
        [MinLength(5)]
        [NotNull]
        [Required]
        public string newPassword { get; set; }
        [Required] 
        public string oldPassword { get; set; }
    }
}
