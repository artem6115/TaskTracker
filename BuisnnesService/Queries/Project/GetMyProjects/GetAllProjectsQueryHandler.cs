using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Project.GetAll
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetMyProjectsQuery,List<ProjectDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public GetAllProjectsQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectDto>> Handle(GetMyProjectsQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.GetMyProjectsAsync();
            return _mapper.Map<List<ProjectDto>>(result);
        }
    }
}
