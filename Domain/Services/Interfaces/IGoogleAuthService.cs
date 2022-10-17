using System;
using Domain.Services.Models;

namespace Domain.Services.Interfaces
{
    public interface IGoogleAuthService
    {
        Task<GoogleAuthOut> VerifyGoogleTokenAsync(string googleToken);
    }
}

