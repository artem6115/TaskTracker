using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Comment.GetCommentsForTask
{
    public class GetCommentsForTaskQuery : IRequest<List<Infrastructure.Entities.Comment>>
    {
        public long Id { get; set; }
    }
}
