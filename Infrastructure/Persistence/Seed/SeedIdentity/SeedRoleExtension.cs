using System;
using Domain.Entities.Users;
using Domain.Entities.Users.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed;

public static class SeedRoleExtension
{
    public static async Task SeedRole(this RoleManager<Role> roleManager)
    {
        var roles = new List<Role>()
    {
        new()
        {
            Name = Roles.User,
        },
        new()
        {
            Name = Roles.Admin,
        },
        new()
        {
            Name = Roles.Volunteer,
        },
        new()
        {
            Name = Roles.Owner,
        },
    };

        foreach (var role in roles)
        {
            var entity = await roleManager.FindByNameAsync(role.Name);
            if (entity is null)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}

