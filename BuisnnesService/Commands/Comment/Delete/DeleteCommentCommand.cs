using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Comment.Delete
{
    public class DeleteCommentCommand : IRequest
    {
        public long Id { get; set; }
    }
}
