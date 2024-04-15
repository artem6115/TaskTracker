namespace BuisnnesService.Commands.Notes.Update
{
    public class NoteUpdateCommand : IRequest<NoteDto>
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreated { get; private set; }
        public DateTime? DateOfChanged { get; set; }
    }
}
