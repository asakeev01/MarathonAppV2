using Domain.Entities.Marathons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{

    public class MarathonConfiguration : IEntityTypeConfiguration<Marathon>
    {
        public void Configure(EntityTypeBuilder<Marathon> builder)
        {
            builder
            .HasOne(x => x.Logo)
            .WithOne(x => x.MarathonLogo)
            .HasForeignKey<Marathon>(x => x.LogoId)
            .OnDelete(DeleteBehavior.SetNull);
            ;
        }
    }
}
