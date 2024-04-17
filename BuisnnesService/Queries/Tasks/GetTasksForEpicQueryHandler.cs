using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Tasks
{
    public class GetTasksForEpicQueryHandler : IRequestHandler<GetTasksForEpicQuery, List<TaskView>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetTasksForEpicQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<List<TaskView>> Handle(GetTasksForEpicQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetTasksForEpicAsync(request.Id);
            return _mapper.Map<List<TaskView>>(tasks);
        }
    }
}
