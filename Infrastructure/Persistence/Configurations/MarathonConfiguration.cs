using Domain.Entities.Marathons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MarathonConfiguration : IEntityTypeConfiguration<Marathon>
{
    public void Configure(EntityTypeBuilder<Marathon> builder)
    {
        builder
        .HasMany(s => s.Applications)
        .WithOne(sc => sc.Marathon)
        .OnDelete(DeleteBehavior.Restrict);

        builder
        .HasMany(s => s.Distances)
        .WithOne(sc => sc.Marathon)
        .OnDelete(DeleteBehavior.ClientCascade);

        builder
        .HasMany(s => s.MarathonTranslations)
        .WithOne(sc => sc.Marathon)
        .OnDelete(DeleteBehavior.ClientCascade);

        builder
        .HasMany(s => s.Partners)
        .WithOne(sc => sc.Marathon)
        .OnDelete(DeleteBehavior.ClientCascade);

        builder
        .HasMany(s => s.Vouchers)
        .WithOne(sc => sc.Marathon)
        .OnDelete(DeleteBehavior.Restrict);



        builder
        .HasMany(s => s.Documents)
        .WithOne(sc => sc.Marathon)
        .OnDelete(DeleteBehavior.ClientCascade);
    }
}
