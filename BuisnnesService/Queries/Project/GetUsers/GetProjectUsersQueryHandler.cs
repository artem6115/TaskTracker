using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Project.GetUsers
{
    public class GetProjectUsersQueryHandler : IRequestHandler<GetProjectUsersQuery, List<UserClaims>>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public GetProjectUsersQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }
        public async Task<List<UserClaims>> Handle(GetProjectUsersQuery request, CancellationToken cancellationToken)
            => _mapper.Map<List<UserClaims>>(await _projectRepository.GetUsers(request.ProjectId));
    }
}
