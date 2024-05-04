using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Epics
{
    public class GetEpicQueryHandler : IRequestHandler<GetEpicQuery,EpicDto>
    {

        private readonly IEpicRepository _epicRepository;
        private readonly IMapper _mapper;

        public GetEpicQueryHandler(IEpicRepository repository, IMapper mapper)
        {
            _epicRepository = repository;
            _mapper = mapper;
        }

        public async Task<EpicDto> Handle(GetEpicQuery request, CancellationToken cancellationToken)
            => _mapper.Map<EpicDto>(await _epicRepository.GetEpicAsync(request.Id));
        
    }
}
