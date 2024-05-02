using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Project.GetProject
{
    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery,ProjectDetails>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public GetProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDetails> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        { 
            var entity = await _projectRepository.GetProjectAsync(request.Id);
            var dto = _mapper.Map<ProjectDetails>(entity);
            dto.Users = _mapper.Map<List<UserClaims>>(await _projectRepository.GetUsers(request.Id));
            return dto;
        }

    }
}
