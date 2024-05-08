using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Comment.Create
{
    public class CreateCommentCommand : IRequest<Infrastructure.Entities.Comment>
    {
        public string Description { get; set; }
        public long WorkTaskId { get; set; }
    }
}
