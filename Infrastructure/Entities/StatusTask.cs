namespace Infrastructure.Entities
{
    public class StatusTask
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; set; }
        public long ProjectTaskId { get; set; }
        public ProjectTask? ProjectTask { get; set; }
        public int OrderNumber { get; set; }

    }
}