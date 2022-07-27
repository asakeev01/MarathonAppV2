using System;
using System.ComponentModel.DataAnnotations;
using MarathonApp.DAL.Enums;

namespace MarathonApp.Models.Profiles
{
    public class ProfileCreateViewModel
    {
        //[StringLength(50)]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        public TshirtEnum? Tshirt { get; set; }

        [Required]
        public CountriesEnum? Country { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string? ExtraPhoneNumber { get; set; }
    }
}

