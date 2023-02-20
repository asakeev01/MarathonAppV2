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

        builder
            .HasOne(x => x.Language)
            .WithMany(x => x.MarathonTranslations)
            .HasForeignKey(x => x.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Logo)
            .WithOne(x => x.MarathonLogo)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
