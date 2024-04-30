using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Project.GetParticipateProjects
{
    public class GetParticipateProjectsQueryHandler : IRequestHandler<GetParticipateProjectsQuery, List<ProjectDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public GetParticipateProjectsQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }
        public async Task<List<ProjectDto>> Handle(GetParticipateProjectsQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.GetParticipateProjectsAsync();
            return _mapper.Map<List<ProjectDto>>(result);
        }
    }
}
