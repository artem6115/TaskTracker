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
        private readonly INotifyRepostitory _notifyRepostitory;

        public UpdateStatusTaskCommandHandler(INotifyRepostitory notifyRepostitory, ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _notifyRepostitory = notifyRepostitory;
        }
        public async Task<TaskView> Handle(UpdateStatusTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskAsync(request.Id);
            WorkTask updatedTask;
            task = _mapper.Map(request, task);
            updatedTask = await _taskRepository.UpdateStatusTaskAsync(task);
            if (task.Epic is not null)
            {
                var notify = new Notify()
                {
                    Message = $"Статус задачи {task.Title} , был изменен",
                    UserId = task.Epic.Project.AuthorId,
                };
                _notifyRepostitory.CreatelNotify(notify);
                            }
            return _mapper.Map<TaskView>(updatedTask);
        }
    }
}
