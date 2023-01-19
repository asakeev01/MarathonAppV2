using Domain.Entities.Documents;
using Domain.Entities.Statuses;
using Domain.Entities.Users;
using Domain.Entities.Users.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed;

public static class SeedUserExtension
{
    public static async Task SeedUser(this UserManager<User> userManager)
    {
        var owner = new User
        {
            Email = Owner.Email,
            UserName = Owner.Email,
            EmailConfirmed = true,
            Documents = new List<Document>(),
        };
        owner.Documents.Add(new Document());
        owner.Status = new Status();

        PasswordHasher<User> ph = new PasswordHasher<User>();
        owner.PasswordHash = ph.HashPassword(owner, "Aidar");

        var entity = await userManager.FindByEmailAsync(owner.Email);
        if (entity is null)
        {
            await userManager.CreateAsync(owner);
            await userManager.AddToRoleAsync(owner, Roles.Owner);
        }
    }
    
}