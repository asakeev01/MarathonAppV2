using Domain.Entities.Accounts;
using Domain.Entities.Applications;
using Domain.Entities.Documents;
using Domain.Entities.Users.UserEnums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Users;

public class User : IdentityUser<long>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DateOfBirth { get; set; }

    public bool NewUser { get; set; }
    public GenderEnum? Gender { get; set; }
    public TshirtEnum? Tshirt { get; set; }
    public CountriesEnum? Country { get; set; }
    public string? ExtraPhoneNumber { get; set; }

    public Document Document { get; set; }
    public virtual ICollection<Application> Applications { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }

    public ICollection<Account> Accounts { get; set; }

    public bool IsDeleted { get; set; } = false;
}
