using System;
using Domain.Entities.Statuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StatusCommentConfiguration : IEntityTypeConfiguration<StatusComment>
{
    public void Configure(EntityTypeBuilder<StatusComment> builder)
    {
        builder
            .HasOne(sc => sc.Comment)
            .WithMany(c => c.StatusComments)
            .HasForeignKey(sc => sc.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(sc => sc.Status)
            .WithMany(s => s.StatusComments)
            .HasForeignKey(sc => sc.StatusId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


