using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    [Table("Tasks")]
    public class ProjectTask
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public Project? Project { get; set; }
        public HashSet<ProjectTask>? InternalTasks { get; set; }
        public HashSet<StatusTask>? StatusTask { get; set; }

    }
}