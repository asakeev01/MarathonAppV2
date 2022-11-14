using Domain.Entities.Users;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Seed;

public static class SeedDataExtension
{
    public static async Task SeedData(this AppDbContext dbContext)
    {
        await dbContext.SeedLanguage();
        await dbContext.SaveChangesAsync();
    }
}