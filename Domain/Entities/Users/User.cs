using Domain.Entities.Applications;
using Domain.Entities.Documents;
using Domain.Entities.Statuses;
using Domain.Entities.Users.UserEnums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Users;

public class User : IdentityUser<long>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public bool? Gender { get; set; }
    public string? ExtraPhoneNumber { get; set; }
    public bool IsDisable { get; set; }
    public bool IsDeleted { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfConfirmation { get; set; }
    public TshirtEnum? Tshirt { get; set; }
    public CountriesEnum? Country { get; set; }

    public Status Status { get; set; }
    public ICollection<Document> Documents { get; set; }

    public ICollection<Application> Applications { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }

    public int GetAge(DateTime? marathonTime=null) {

        var date = marathonTime ?? DateTime.Today;

        int age = date.Year - DateOfBirth.Value.Year;

        if (DateOfBirth.Value > date.AddYears(-age)) age--;

        return age;
    }
}
