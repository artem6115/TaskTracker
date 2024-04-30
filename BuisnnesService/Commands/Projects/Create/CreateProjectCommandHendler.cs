using BuisnnesService.Commands.Projects.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Projects.Create
{
    public class CreateProjectCommandHendler : IRequestHandler<CreateProjectCommand, ProjectDto>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public CreateProjectCommandHendler(IProjectRepository projectRepository, IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }
        public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = await _projectRepository.CreateProjectAsync(_mapper.Map<Project>(request));
            return _mapper.Map<ProjectDto>(entity);
        }
    }
}
