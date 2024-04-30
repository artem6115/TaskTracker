using BuisnnesService.Commands.Tasks.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Tasks.Delete
{
    public class DeleteTaskCommandHendler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        public DeleteTaskCommandHendler(ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
            => await _taskRepository.DeleteTaskAsync(request.Id);

        
    }
}
