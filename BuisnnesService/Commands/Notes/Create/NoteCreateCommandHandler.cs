using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Notes.Create
{
    public class NoteCreateCommandHandler : IRequestHandler<NoteCreateCommand, NoteDto>
    {
        private readonly IMapper _mapper;
        private readonly INoteRepository _noteRepository;
        public NoteCreateCommandHandler(INoteRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _noteRepository = repository;
        }
        public async Task<NoteDto> Handle(NoteCreateCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.CreateAsync(_mapper.Map<Note>(request));
            return _mapper.Map<NoteDto>(note);
        }
    }
}
