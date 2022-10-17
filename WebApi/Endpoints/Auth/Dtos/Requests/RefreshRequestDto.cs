using System;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests
{
    public class RefreshRequestDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }

    public class RefreshRequestValidator : AbstractValidator<RefreshRequestDto>
    {
        public RefreshRequestValidator()
        {
        }
    }
}

