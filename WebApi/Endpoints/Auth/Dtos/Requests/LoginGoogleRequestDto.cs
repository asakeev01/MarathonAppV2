using System;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests
{
    public class LoginGoogleRequestDto
    {
        public string googleToken { get; set; }
    }

    public class LoginGoogleRequestValidator : AbstractValidator<LoginGoogleRequestDto>
    {
        public LoginGoogleRequestValidator()
        {

        }
    }
}

