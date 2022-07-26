using System;
using System.ComponentModel.DataAnnotations;
using MarathonApp.DAL.Enums;
using Microsoft.AspNetCore.Identity;

namespace MarathonApp.DAL.Entities
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        public bool NewUser { get; set; }
        public bool Gender { get; set; }
        public TshirtEnum? Tshirt { get; set; }
        public CountriesEnum? Country { get; set; }
        public string? ExtraPhoneNumber { get; set; }

        public ImagesEntity Images {get; set;}
    }
}
