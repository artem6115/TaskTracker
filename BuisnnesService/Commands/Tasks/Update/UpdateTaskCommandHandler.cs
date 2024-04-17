using BuisnnesService.Commands.Tasks.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Tasks.Update
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskView>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        public UpdateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<TaskView> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var newTask = await _taskRepository.UpdateTaskAsync(_mapper.Map<WorkTask>(request));
            return _mapper.Map<TaskView>(newTask);
        }
    }
}
