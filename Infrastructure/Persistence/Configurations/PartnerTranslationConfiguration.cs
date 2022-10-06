using Domain.Entities.Marathons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PartnerTranslationConfiguration : IEntityTypeConfiguration<PartnerTranslation>
{
    public void Configure(EntityTypeBuilder<PartnerTranslation> builder)
    {
        builder
            .HasIndex(p => new { p.PartnerId, p.LanguageId })
            .IsUnique();
        builder.HasOne(x => x.Partner)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.PartnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
