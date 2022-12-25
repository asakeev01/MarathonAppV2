using System;
using Domain.Entities.Statuses;
using Domain.Entities.Statuses.StatusEnums;
using Domain.Entities.Users;
using Domain.Entities.Users.UserEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder
            .HasMany(s => s.StatusComments)
            .WithOne(sc => sc.Status)
            .OnDelete(DeleteBehavior.Cascade);
  

        builder
            .Property(p => p.CurrentStatus)
            .HasDefaultValue(StatusesEnum.Empty);
    }
}


