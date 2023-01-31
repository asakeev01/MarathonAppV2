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


        builder
        .HasOne(x => x.Language)
        .WithMany(x => x.PartnerTranlations)
        .HasForeignKey(x => x.LanguageId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
