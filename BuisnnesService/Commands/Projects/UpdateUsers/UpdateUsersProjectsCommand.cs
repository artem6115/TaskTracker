using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Projects.UpdateUsers
{
    public class UpdateUsersProjectsCommand : IRequest
    {
        public long ProjectId { get; set; }
        public List<long> UsersIdToAdd { get; set; }
        public List<long> UsersIdToRemove { get; set; }

    }
}
