using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Tasks.Update
{
    public class UpdateStatusTaskCommandHandler : IRequestHandler<UpdateStatusTaskCommand, TaskView>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        public UpdateStatusTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<TaskView> Handle(UpdateStatusTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskAsync(request.Id);
            if (request.StatusTask == Infrastructure.Entities.TaskStatus.Completed &&
                task.StatusTask != Infrastructure.Entities.TaskStatus.Completed)
                task.DateOfClosed = DateTime.UtcNow;
            task = _mapper.Map(request, task);
            var newTask = await _taskRepository.UpdateTaskAsync(task);
            return _mapper.Map<TaskView>(newTask);
        }
    }
}
