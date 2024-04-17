using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Tasks
{
    public class GetMyTasksQueryHandler : IRequestHandler<GetMyTasksQuery, List<TaskView>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetMyTasksQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<List<TaskView>> Handle(GetMyTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetMyTasksAsync();
            return _mapper.Map<List<TaskView>>(tasks);
        }
    }
}
