using Domain.Entities.Marathons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {
        builder
        .HasMany(s => s.PartnerCompanies)
        .WithOne(sc => sc.Partner)
        .OnDelete(DeleteBehavior.ClientCascade);
    }
}
