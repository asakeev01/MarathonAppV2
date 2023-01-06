using System;
using Domain.Common.Options;
using Domain.Entities.Users.Exceptions;
using Domain.Services.Interfaces;
using Domain.Services.Models;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace Domain.Services;

public class GoogleAuthService : IGoogleAuthService
{
    private GoogleAuthOptions _googleAuthOptions;

    public GoogleAuthService(IOptionsMonitor<GoogleAuthOptions> googleAuthOptions)
    {
        _googleAuthOptions = googleAuthOptions.CurrentValue;
    }
    public async Task<GoogleAuthOut> VerifyGoogleTokenAsync(string googleToken)
    {
        try
        {
            var clientId = _googleAuthOptions.ClientId;
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _googleAuthOptions.ClientId }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(googleToken, settings);
            var googleAuthOut = new GoogleAuthOut
            {
                Id = payload.Subject,
                Email = payload.Email,
                Name = payload.Name
            };
            return googleAuthOut;
        }
        catch (Exception ex)
        {
            throw new InvalidTokenException();
        }
    }
}


