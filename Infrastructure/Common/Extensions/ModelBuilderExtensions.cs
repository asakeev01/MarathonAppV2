using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Extensions;

public static class ModelBuilderExtensions
{
    public static void AddIsDeletedQuery(this ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasQueryFilter(p => !p.IsDeleted);
    }
}