using System;
using System.ComponentModel.DataAnnotations;
using MarathonApp.DAL.Enums;

namespace MarathonApp.Models.Users
{
    public class LoginViewModel
    {
        public class LoginIn
        {
            [Required]
            [StringLength(50)]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(50, MinimumLength = 5)]
            public string Password { get; set; }
        }

        public class LoginOut
        {
            public Guid UserId { get; set; }
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
            public DateTime AccessTokenExpireUtc { get; set; }
            public DateTime RefreshTokenExpireUtc { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }

        }
    }
}

