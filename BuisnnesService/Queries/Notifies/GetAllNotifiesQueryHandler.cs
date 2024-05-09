using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Notifies
{
    public class GetAllNotifiesQueryHandler : IRequestHandler<GetAllNotifiesQuery, List<Notify>>
    {
        private readonly INotifyRepostitory _notifyRepository;
        private readonly IMapper _mapper;

        public GetAllNotifiesQueryHandler(INotifyRepostitory repository, IMapper mapper)
        {
            _notifyRepository = repository;
            _mapper = mapper;
        }
        public async Task<List<Notify>> Handle(GetAllNotifiesQuery request, CancellationToken cancellationToken)
            => await _notifyRepository.GetAllNotifies();
    }
}
