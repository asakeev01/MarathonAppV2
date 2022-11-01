using System;
using Domain.Entities.Users;
using Domain.Entities.Users.UserEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithOne(u => u.Status)
                .HasForeignKey<Status>(x => x.UserId);
            builder
                .Property(p => p.CurrentStatus)
                .HasDefaultValue(StatusesEnum.Empty);
            builder
                .Property(p => p.Comment)
                .HasDefaultValue(CommentsEnum.Empty);
        }
    }
}

