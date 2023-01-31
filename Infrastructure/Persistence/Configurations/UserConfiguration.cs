using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(u => u.IsDeleted)
            .HasDefaultValue(false);

        builder
            .HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.Documents)
            .WithOne(d => d.User)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(u => u.Status)
            .WithOne(s => s.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}