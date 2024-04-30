using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Project.GetProject
{
    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery,Infrastructure.Entities.Project>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public GetProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<Infrastructure.Entities.Project> Handle(GetProjectQuery request, CancellationToken cancellationToken)
            => await _projectRepository.GetProjectAsync(request.Id);
    }
}
