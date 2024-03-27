using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntitiesConfigurations
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           builder.Property(x=>x.AccessFaildCount).HasDefaultValue(0);
           builder.Property(x => x.Confirmed).HasDefaultValue(false);
           builder.Property(x => x.Banned).HasDefaultValue(false);
           builder.Property(x => x.Deleted).HasDefaultValue(false);
            builder.HasIndex(x => x.Email).IsUnique();

        }
    }
}
