using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Epics.Update
{
    public class UpdateEpicCommandHandler : IRequestHandler<UpdateEpicCommand,EpicDto>
    {
        private readonly IEpicRepository _epicRepository;
        private readonly IMapper _mapper;

        public UpdateEpicCommandHandler(IEpicRepository repository, IMapper mapper)
        {
            _epicRepository = repository;
            _mapper = mapper;
        }

        public async Task<EpicDto> Handle(UpdateEpicCommand request, CancellationToken cancellationToken)
        {
            var entity = await _epicRepository.GetEpicAsync(request.Id);
            if (entity.Project.AuthorId != UserClaims.User.Id)
                throw new AccessViolationException("У вас нет прав на изменение данного эпика");
            var newEntity = await _epicRepository.UpdateEpicAsync(_mapper.Map(request,entity));
            return _mapper.Map<EpicDto>(newEntity);
        }
    }
}
