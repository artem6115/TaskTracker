
namespace BuisnnesService.Queries.Notes
{
    public class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, NoteDto>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetNoteQueryHandler(INoteRepository repository, IMapper mapper)
        { 
            _noteRepository = repository;
            _mapper = mapper;
        }
        public async Task<NoteDto> Handle(GetNoteQuery request, CancellationToken cancellationToken)
        { 
            var note =  await _noteRepository.GetNoteAsync(request.Id);
            return _mapper.Map<NoteDto>(note);
        }
    }
}
