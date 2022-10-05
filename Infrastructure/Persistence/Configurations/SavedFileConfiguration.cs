using Domain.Entities.SavedFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class SavedFileConfiguration : IEntityTypeConfiguration<SavedFile>
    {
        public void Configure(EntityTypeBuilder<SavedFile> builder)
        {
            builder.HasOne(x => x.Partner)
                .WithMany(x => x.Logos)
                .HasForeignKey(x => x.PartnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
