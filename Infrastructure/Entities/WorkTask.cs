using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    [Table("Tasks")]
    public class WorkTask 
    {
        public long Id { get; private set; }
        public byte? Importance { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        public DateTime DateOfCreated { get;private set; }
        public DateTime? DateOfClosed { get; set; }
        public DateTime? ApproximateDateOfCompleted { get; set; }

        public TaskStatus StatusTask { get; set; }
           
        public WorkTask? PreviousTask {  get; set; }
        public long? PreviousTaskId { get; set; }


        public long? EpicId { get; set; }
        public Epic? Epic { get; set; }
        public long? UserId { get; set; }
        public User? User { get; set; } 

        public List<Attachment> Attachments { get; set; } = null!;
        public List<Comment> Comments { get; set; } = null!;




    }
}