using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Tasks.Create
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskView>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        public CreateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<TaskView> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<WorkTask>(request);
            var newTask = await _taskRepository.CreateTaskAsync(task);
            return _mapper.Map<TaskView>(newTask);
        }
    }
}
