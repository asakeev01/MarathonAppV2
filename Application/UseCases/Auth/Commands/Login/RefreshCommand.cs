using System;
using Core.UseCases.Auth.Commands.Login;
using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Auth.Commands.Login
{
    public class RefreshCommand : IRequest<LoginOutDto>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshCommandHandler : IRequestHandler<RefreshCommand, LoginOutDto>
    {
        private readonly IUnitOfWork _unit;
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;

        public RefreshCommandHandler(IUnitOfWork unit, IUserService userService, IRefreshTokenService refreshTokenService)
        {
            _unit = unit;
            _userService = userService;
            _refreshTokenService = refreshTokenService;

        }

        public async Task<LoginOutDto> Handle(RefreshCommand cmd, CancellationToken cancellationToken)
        {
            _userService.IsAccessTokenValid(cmd.AccessToken);
            var refreshToken = await _unit.RefreshTokenRepository.FirstAsync(x => x.Name == cmd.RefreshToken);
            _refreshTokenService.IsRefreshTokenValid(refreshToken);
            await _unit.RefreshTokenRepository.Delete(refreshToken);
            var identityUser = await _unit.UserRepository.GetByIdAsync(refreshToken.UserId.ToString());
            var roles = await _unit.UserRepository.GetRolesAsync(identityUser);
            var newRefreshToken = _refreshTokenService.GenerateRefreshToken(identityUser.Id);
            await _unit.RefreshTokenRepository.CreateAsync(newRefreshToken, save: true);
            var loginOut = _userService.LoginAsync(identityUser, newRefreshToken, roles);
            var loginOutDto = LoginOutDto.MapFrom(loginOut);
            return loginOutDto;
        }
    }
}

