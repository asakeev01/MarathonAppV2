using System;
using MarathonApp.BLL.Services;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarathonApp.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IEmailService _emailService;

        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet("registerowner")]
        public async Task RegisterOwnerAsync()
        {
            await _userService.RegisterOwnerAsync();
        }

        [HttpPost("registeradmin")]
        [Authorize(Roles = UserRolesModel.Owner)]
        public async Task RegisterAdminAsync(AdminOwnerRegisterModel model)
        {
            await _userService.RegisterAdminAsync(model);
        }

        [HttpPost("register")]
        public async Task RegisterAsync(RegisterViewModel model)
        {
            await _userService.RegisterAsync(model);
        }

        [HttpPost("login")]
        public async Task<LoginViewModel.LoginOut> LoginAsync(LoginViewModel.LoginIn model)
        {
            return await _userService.LoginAsync(model);
        }

        [HttpGet("confirmemail")]
        public async Task ConfirmEmailAsync(string userId, string token)
        {
            await _emailService.ConfirmEmailAsync(userId, token);
        }

        [HttpPost("forgetpassword")]
        public async Task ForgetPasswordAsync(string email)
        {
            await _emailService.ForgetPasswordAsync(email);
        }

        [HttpPost("resetpassword")]
        public async Task ResetPasswordAsync(ResetPasswordViewModel model)
        {
            await _emailService.ResetPasswordAsync(model);
        }

        [HttpPost("refresh")]
        public async Task<LoginViewModel.LoginOut> UseRefreshTokenAsync(LoginViewModel.RefreshIn model)
        {
            return await _userService.UseRefreshTokenAsync(model);
        }
    }
}

