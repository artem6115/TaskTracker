using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Notifies.Delete
{
    public class DeleteNotifyCommandHandler : IRequestHandler<DeleteNotifyCommand>
    {
        private readonly INotifyRepostitory _notifyRepository;
        private readonly IMapper _mapper;

        public DeleteNotifyCommandHandler(INotifyRepostitory repository, IMapper mapper)
        {
            _notifyRepository = repository;
            _mapper = mapper;
        }
        public async Task Handle(DeleteNotifyCommand request, CancellationToken cancellationToken)
            => await _notifyRepository.DeleteNotify(request.Id);
    }
}
