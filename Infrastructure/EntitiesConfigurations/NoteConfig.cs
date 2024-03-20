
namespace Infrastructure.EntitiesConfigurations
{
    internal class NoteConfig : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasOne(x => x.User).WithMany(x=>x.Notes)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
