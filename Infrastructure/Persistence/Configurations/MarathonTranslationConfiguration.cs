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
            .HasOne(x => x.Logo)
            .WithOne(x => x.MarathonLogo)
            .HasForeignKey<MarathonTranslation>(x => x.LogoId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
