
namespace BuisnnesService.Queries.Notes
{
    public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery,List<NoteDto>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetNotesQueryHandler(INoteRepository repository, IMapper mapper)
        {
            _noteRepository = repository;
            _mapper = mapper;
        }
        public async Task<List<NoteDto>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            var notes = await _noteRepository.GetNotesAsync();
            return _mapper.Map<List<NoteDto>>(notes);
        }
    }
}
