using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Notifies.Delete
{
    public class DeleteAllNotifiesCommandHandler : IRequestHandler<DeleteAllNotifiesCommand>
    {
        private readonly INotifyRepostitory _notifyRepository;
        private readonly IMapper _mapper;

        public DeleteAllNotifiesCommandHandler(INotifyRepostitory repository, IMapper mapper)
        {
            _notifyRepository = repository;
            _mapper = mapper;
        }

        public async Task Handle(DeleteAllNotifiesCommand request, CancellationToken cancellationToken)
            => await _notifyRepository.DeleteAllNotifies(DateTime.Now);
    }
}
