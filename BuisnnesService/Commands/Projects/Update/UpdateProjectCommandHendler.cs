using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Projects.Update
{
    public class UpdateProjectCommandHendler : IRequestHandler<UpdateProjectCommand, ProjectDto>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public UpdateProjectCommandHendler(IProjectRepository projectRepository, IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }
        public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = await _projectRepository.GetProjectAsync(request.Id);
            if (entity.AuthorId != UserClaims.User.Id)
                throw new AccessViolationException("Вы не имеете доступа, для редактирования данного проекта");
            var newEntity = await _projectRepository.UpdateProjectAsync(_mapper.Map(request,entity));
            return _mapper.Map<ProjectDto>(newEntity);
        }
    }
}
