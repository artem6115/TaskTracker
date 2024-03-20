using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations
{
    internal class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasMany(x => x.Users).WithMany(x => x.Projects);
            builder.HasOne(x=>x.Author).WithMany(x=>x.Projects)
                .HasForeignKey(x=>x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
