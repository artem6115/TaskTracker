using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Notes.Create
{
    internal class NoteUpdateCommandHandler : IRequestHandler<NoteUpdateCommand, NoteDto>
    {
        private readonly IMapper _mapper;
        private readonly INoteRepository _noteRepository;
        public NoteUpdateCommandHandler(INoteRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _noteRepository = repository;
        }
        public async Task<NoteDto> Handle(NoteUpdateCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetNoteAsync(request.Id);
            var newNote = await _noteRepository.UpdateAsync(_mapper.Map(request,note));
            return _mapper.Map<NoteDto>(newNote);
        }
    }
}
    