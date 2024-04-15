using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Notes.Delete
{
    public class NoteDeleteCommandHandler : IRequestHandler<NoteDeleteCommand>
    {
        private readonly IMapper _mapper;
        private readonly INoteRepository _noteRepository;
        public NoteDeleteCommandHandler(INoteRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _noteRepository = repository;
        }
        public async Task Handle(NoteDeleteCommand request, CancellationToken cancellationToken)
            => await _noteRepository.DeleteAsync(request.Id);
    }
}
