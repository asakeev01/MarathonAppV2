using Domain.Entities.Accounts;
using Domain.Entities.Marathons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MarathonTranslationConfiguration : IEntityTypeConfiguration<MarathonTranslation>
{
    public void Configure(EntityTypeBuilder<MarathonTranslation> builder)
    {
        builder
            .HasIndex(p => new { p.MarathonId, p.LanguageId })
            .IsUnique();
    }
}