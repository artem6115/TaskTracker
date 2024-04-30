using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Projects.UpdateUsers
{
    public class UpdateUsersProjectsCommandHandler : IRequestHandler<UpdateUsersProjectsCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public UpdateUsersProjectsCommandHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }
        public async Task Handle(UpdateUsersProjectsCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectAsync(request.ProjectId);
            if (project.AuthorId != UserClaims.User.Id)
                throw new AccessViolationException("У вас нет доступа на изменение состава команды данного проекта");
            var toAdd = request.UsersIdToAdd.Select(x => new UserProject()
            {
                ProjectId = request.ProjectId,
                UserId = x
            });
            var toRemove = request.UsersIdToRemove.Select(x => new UserProject()
            {
                ProjectId = request.ProjectId,
                UserId = x
            });
            await _projectRepository.ChangeProjectTeam(toAdd,toRemove);

        }
    }
}
