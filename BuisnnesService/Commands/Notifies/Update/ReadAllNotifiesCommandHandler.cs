using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Notifies.Update
{
    public class ReadAllNotifiesCommandHandler : IRequestHandler<ReadAllNotifiesCommand>
    {
        private readonly INotifyRepostitory _notifyRepository;
        private readonly IMapper _mapper;

        public ReadAllNotifiesCommandHandler(INotifyRepostitory repository, IMapper mapper)
        {
            _notifyRepository = repository;
            _mapper = mapper;
        }
        public async Task Handle(ReadAllNotifiesCommand request, CancellationToken cancellationToken)
            => await _notifyRepository.ReadAllNotifies(DateTime.Now);
    }
}
