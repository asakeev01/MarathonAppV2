using System;
using System.ComponentModel.DataAnnotations;

namespace MarathonApp.Models.Profiles
{
    public class ProfilesModel
    {
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}

