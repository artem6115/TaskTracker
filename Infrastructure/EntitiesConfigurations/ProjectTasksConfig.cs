using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.EntitiesConfigurations
{
    internal class ProjectTasksConfig : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.HasOne(x=>x.User)
        }
    }
}
