using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Notifies.Create
{
    public class CreateNotifyCommandHandler : IRequestHandler<CreateNotifyCommand, Notify>
    {
        private readonly INotifyRepostitory _notifyRepository;
        private readonly IMapper _mapper;

        public CreateNotifyCommandHandler(INotifyRepostitory repository, IMapper mapper)
        {
            _notifyRepository = repository;
            _mapper = mapper;
        }
        public async Task<Notify> Handle(CreateNotifyCommand request, CancellationToken cancellationToken)
            => await _notifyRepository.CreatelNotify(_mapper.Map<Notify>(request));
    }
}
