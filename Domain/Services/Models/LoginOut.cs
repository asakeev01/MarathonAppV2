using System;
namespace Domain.Services.Models
{
    public class LoginOut
    {
        public long UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpireUtc { get; set; }
        public DateTime RefreshTokenExpireUtc { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? Name { get; set; }
    }
}

