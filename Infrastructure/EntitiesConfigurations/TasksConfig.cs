using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntitiesConfigurations
{
    internal class TasksConfig : IEntityTypeConfiguration<WorkTask>
    {
        public void Configure(EntityTypeBuilder<WorkTask> builder)
        {
            builder.ToTable(x => x.HasTrigger("UpdateTaskTriger"));
            builder.HasOne(x => x.User).WithMany(x => x.Tasks)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Epic).WithMany(x => x.Tasks)
                .HasForeignKey(x=>x.EpicId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
