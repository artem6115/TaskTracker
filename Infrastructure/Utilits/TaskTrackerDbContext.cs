


namespace Infrastructure.Utilits;
public partial class TaskTrackerDbContext : DbContext
{
    public  TaskTrackerDbContext()
    {

    }

    public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options)
        : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<UserProject> UsersProjects { get; set; }

    public DbSet<Note> Notes { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<WorkTask> Tasks { get; set; }
    public DbSet<Epic> Epics { get; set; }
    public DbSet<Notify> Notifies { get; set; }





    protected async override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
     
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskTrackerDbContext).Assembly);
    }


}
