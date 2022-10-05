using Domain.Entities.Marathons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class PartnerTranslationConfiguration : IEntityTypeConfiguration<PartnerTranslation>
    {
        public void Configure(EntityTypeBuilder<PartnerTranslation> builder)
        {
            builder
                .HasIndex(p => new { p.PartnerId, p.LanguageId })
                .IsUnique();
        }
    }
}
