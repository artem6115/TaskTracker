
namespace BuisnnesService.Queries.Notes
{
    public class GetNoteQuery : IRequest<NoteDto>
    {
        public long Id { get; set; }
    }
}
