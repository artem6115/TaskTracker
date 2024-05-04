using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Epics.Create
{
    public class CreateEpicCommandHandler : IRequestHandler<CreateEpicCommand, EpicDto>
    {
        private readonly IEpicRepository _epicRepository;
        private readonly IMapper _mapper;

        public CreateEpicCommandHandler(IEpicRepository repository, IMapper mapper)
        {
            _epicRepository = repository;
            _mapper = mapper;
        }
        public async Task<EpicDto> Handle(CreateEpicCommand request, CancellationToken cancellationToken)
        {
            var entity = await _epicRepository.CreateEpicAsync(_mapper.Map<Epic>(request));
            return _mapper.Map<EpicDto>(entity);
        }
    }
}
