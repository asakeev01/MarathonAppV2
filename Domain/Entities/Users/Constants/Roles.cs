using System;
namespace Domain.Entities.Users.Constants;

public class Roles
{
    public const string User = "User";
    public const string Admin = "Admin";
    public const string Volunteer = "Volunteer";
    public const string Owner = "Owner";

    public static List<string> GetAdminAndVolunteerRoles()
    {
        return new List<string> { Admin, Volunteer};
    }

    public static List<string> GetUserRole()
    {
        return new List<string> { User };
    }
}

