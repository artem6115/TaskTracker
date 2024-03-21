using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    [Table("Tasks")]
    public class ProjectTask
    {
        public ProjectTask()=> DateOfCreated = DateTime.Now;
        public long Id { get; private set; }
        public byte? Importance { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        public DateTime DateOfCreated { get;private set; }
        public DateTime? DateOfClosed { get; set; }
        public DateTime? ApproximateDateOfCompleted { get; set; }

        public TaskStatus StatusTask { get; set; }

        public long? UserId { get; set; }
        public User? User { get; set; }

        public long? PreviousTaskId { get; set; }
        public ProjectTask? PreviousTask {  get; set; }

        public required long EpicId { get; set; }
        public Epic Epic { get; set; } = null!;

        public List<Attachments> Attachments { get; set; } = null!;
        public List<Comments> Comments { get; set; } = null!;




    }
}