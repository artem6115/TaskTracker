using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Projects.Delete
{
    public class DeleteProjectCommandHendler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public DeleteProjectCommandHendler(IProjectRepository projectRepository, IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }
        public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            => await _projectRepository.DeleteProjectAsync(request.Id);
        
    }
}
