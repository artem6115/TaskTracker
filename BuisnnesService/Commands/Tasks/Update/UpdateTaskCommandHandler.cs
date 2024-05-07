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
            var task = await _taskRepository.GetTaskAsync(request.Id);
            WorkTask updatedTask;
            if (request.UserId is null)
            {
                if (task.StatusTask != Infrastructure.Entities.TaskStatus.Blocked ||
                    task.StatusTask != Infrastructure.Entities.TaskStatus.Completed)
                    request.StatusTask = Infrastructure.Entities.TaskStatus.Free;
            }
            else
            {
                if (task.StatusTask == Infrastructure.Entities.TaskStatus.Free)
                    request.StatusTask = Infrastructure.Entities.TaskStatus.Work;
            }
            task = _mapper.Map(request, task);
            updatedTask = await _taskRepository.UpdateTaskAsync(task);
            return _mapper.Map<TaskView>(updatedTask);
        }
    }
}
