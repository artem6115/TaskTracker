using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Epics
{
    public class GetProjectEpicsQueryHandler : IRequestHandler<GetProjectEpicsQuery, List<EpicDto>>
    {
        private readonly IEpicRepository _epicRepository;
        private readonly IMapper _mapper;

        public GetProjectEpicsQueryHandler(IEpicRepository repository, IMapper mapper)
        {
            _epicRepository = repository;
            _mapper = mapper;
        }
        public async Task<List<EpicDto>> Handle(GetProjectEpicsQuery request, CancellationToken cancellationToken)
            => _mapper.Map<List<EpicDto>>(await _epicRepository.GetProjectEpicsAsync(request.Id));
    }
}
