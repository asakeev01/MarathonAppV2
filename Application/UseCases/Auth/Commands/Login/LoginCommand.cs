using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Auth.Commands.Login
{
    public class LoginCommand : IRequest<LoginOutDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginOutDto>
    {
        private readonly IUnitOfWork _unit;
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;

        public LoginCommandHandler(IUnitOfWork unit, IUserService userService, IRefreshTokenService refreshTokenService)
        {
            _unit = unit;
            _userService = userService;
            _refreshTokenService = refreshTokenService;

        }

        public async Task<LoginOutDto> Handle(LoginCommand cmd, CancellationToken cancellationToken)
        {
            var identityUser = await _unit.UserRepository.GetByEmailAsync(cmd.Email);
            await _unit.UserRepository.CheckPasswordAsync(identityUser, cmd.Password);
            var roles = await _unit.UserRepository.GetRolesAsync(identityUser);
            var refreshToken = _refreshTokenService.GenerateRefreshToken(identityUser.Id);
            await _unit.RefreshTokenRepository.CreateAsync(refreshToken, save: true) ;
            var loginOut = _userService.LoginAsync(identityUser, refreshToken, roles);
            var loginOutDto = LoginOutDto.MapFrom(loginOut);
            return loginOutDto;
        }
    }
}

