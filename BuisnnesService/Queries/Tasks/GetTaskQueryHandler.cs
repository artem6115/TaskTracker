using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Tasks
{
    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetTaskQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<TaskDto> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        { 
            var task = await _taskRepository.GetTaskAsync(request.Id);
            return _mapper.Map<TaskDto>(task);
        }
    }
}
