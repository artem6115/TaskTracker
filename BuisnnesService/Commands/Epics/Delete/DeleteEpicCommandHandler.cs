using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Epics.Delete
{
    public class DeleteEpicCommandHandler : IRequestHandler<DeleteEpicCommand>
    {
        private readonly IEpicRepository _epicRepository;

        public DeleteEpicCommandHandler(IEpicRepository repository)
        {
            _epicRepository = repository;
        }

        public async Task Handle(DeleteEpicCommand request, CancellationToken cancellationToken)
            => await _epicRepository.DeleteEpicAsync(request.Id);
    }
}
