using Domain.Entities.Marathons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MarathonConfiguration : IEntityTypeConfiguration<Marathon>
{
    public void Configure(EntityTypeBuilder<Marathon> builder)
    {
    }
}
