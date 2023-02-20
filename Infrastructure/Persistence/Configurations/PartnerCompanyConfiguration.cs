using Domain.Entities.Marathons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PartnerCompanyConfiguration : IEntityTypeConfiguration<PartnerCompany>
{
    public void Configure(EntityTypeBuilder<PartnerCompany> builder)
    {
        builder
            .HasOne(x => x.Logo)
            .WithOne(x => x.PartnerCompany)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
