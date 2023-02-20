using Domain.Entities.SavedFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class SavedFileConfiguration : IEntityTypeConfiguration<SavedFile>
{
    public void Configure(EntityTypeBuilder<SavedFile> builder)
    {
        //builder.HasOne(x => x.Partner)
        //    .WithMany(x => x.PartnerCompanies)
        //    .HasForeignKey(x => x.PartnerId)
        //    .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Marathon)
            .WithMany(x => x.Documents)
            .HasForeignKey(x => x.MarathonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.PartnerCompany)
            .WithOne(x => x.Logo)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
