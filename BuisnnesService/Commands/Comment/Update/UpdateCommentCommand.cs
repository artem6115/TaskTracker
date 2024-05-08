using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Comment.Update
{
    public class UpdateCommentCommand : IRequest<Infrastructure.Entities.Comment>
    {
        public long Id { get; set; }
        public string Description { get; set; }
    }
}
