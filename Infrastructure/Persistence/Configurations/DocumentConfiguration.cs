using System;
using Domain.Entities.Documents;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithOne(u => u.Document)
                .HasForeignKey<Document>(x => x.UserId);
        }
    }
}

