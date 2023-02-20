using System;
using Core.Common.Bases;
using Domain.Services.Models;

namespace Core.UseCases.Auth.Commands.Login
{
    public record LoginOutDto : BaseDto<LoginOut, LoginOutDto>
    {
        public long UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpireUtc { get; set; }
        public DateTime RefreshTokenExpireUtc { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
    }
}

