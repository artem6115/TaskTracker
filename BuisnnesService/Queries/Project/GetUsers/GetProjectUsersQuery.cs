using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Project.GetUsers
{
    public class GetProjectUsersQuery : IRequest<List<UserClaims>>
    {
        public long ProjectId { get; set; }
    }
}
