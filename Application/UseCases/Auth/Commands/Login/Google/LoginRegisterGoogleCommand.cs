using System;
using Domain.Common.Contracts;
using Domain.Entities.Documents;
using Domain.Entities.Users;
using Domain.Entities.Users.Constants;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Auth.Commands.Login.Google
{
    public class LoginRegisterGoogleCommand : IRequest<LoginOutDto>
    {
        public string googleToken { get; set; }
    }

    public class LoginRegisterGoogleHandler : IRequestHandler<LoginRegisterGoogleCommand, LoginOutDto>
    {
        private readonly IUnitOfWork _unit;
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IGoogleAuthService _googleAuthService;

        public LoginRegisterGoogleHandler(IUnitOfWork unit, IUserService userService, IRefreshTokenService refreshTokenService, IGoogleAuthService googleAuthService)
        {
            _unit = unit;
            _userService = userService;
            _refreshTokenService = refreshTokenService;
            _googleAuthService = googleAuthService;

        }

        public async Task<LoginOutDto> Handle(LoginRegisterGoogleCommand cmd, CancellationToken cancellationToken)
        {
            var googleAuthOut = await _googleAuthService.VerifyGoogleTokenAsync(cmd.googleToken);
            var isExist = await _unit.UserRepository.IsUserExistsAsync(googleAuthOut.Email);
            if (isExist == false)
            {
                var user = _userService.CreateUser(googleAuthOut.Email);
                await _unit.UserRepository.CreateUserAsync(user, "hello123");
                await _unit.UserRepository.AddToRoleAsync(user, Roles.User);
            }
            var identityUser = await _unit.UserRepository.GetByEmailAsync(googleAuthOut.Email);
            var roles = await _unit.UserRepository.GetRolesAsync(identityUser);
            var refreshToken = _refreshTokenService.GenerateRefreshToken(identityUser.Id);
            await _unit.RefreshTokenRepository.CreateAsync(refreshToken, save: true);
            var loginOut = _userService.LoginAsync(identityUser, refreshToken, roles);
            var loginOutDto = LoginOutDto.MapFrom(loginOut);
            loginOutDto.Name = googleAuthOut.Name;
            return loginOutDto;
        }
    }
}

