using Domain.Entities.Distances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DistanceConfiguration : IEntityTypeConfiguration<Distance>
{
    public void Configure(EntityTypeBuilder<Distance> builder)
    {
        builder
        .HasMany(s => s.DistanceAges)
        .WithOne(sc => sc.Distance)
        .OnDelete(DeleteBehavior.ClientCascade);

        builder
        .HasMany(s => s.DistancePrices)
        .WithOne(sc => sc.Distance)
        .OnDelete(DeleteBehavior.ClientCascade);

        builder
        .HasMany(s => s.Promocodes)
        .WithOne(sc => sc.Distance)
        .OnDelete(DeleteBehavior.NoAction);

    }
}
