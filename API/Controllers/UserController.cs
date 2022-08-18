using System;
using MarathonApp.BLL.Services;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Users;

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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("registerowner")]
        public async Task RegisterOwnerAsync()
        {
            await _userService.RegisterOwnerAsync();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("registeradmin")]
        [Authorize(Roles = UserRolesModel.Owner)]
        public async Task RegisterAdminAsync(AdminOwnerRegisterModel model)
        {
            await _userService.RegisterAdminAsync(model);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        public async Task RegisterAsync(RegisterModel model)
        {
            await _userService.RegisterAsync(model);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<ObjectResult> LoginAsync(LoginModel.LoginIn model)
        {
            var result = await _userService.LoginAsync(model);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("confirmemail")]
        public async Task ConfirmEmailAsync(string userId, string token)
        {
            await _emailService.ConfirmEmailAsync(userId, token);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("forgetpassword")]
        public async Task ForgetPasswordAsync(string email)
        {
            await _emailService.ForgetPasswordAsync(email);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("resetpassword")]
        public async Task ResetPasswordAsync(ResetPasswordModel model)
        {
            await _emailService.ResetPasswordAsync(model);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("refresh")]
        public async Task<ObjectResult> UseRefreshTokenAsync(LoginModel.RefreshIn model)
        {
            var result = await _userService.UseRefreshTokenAsync(model);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("sendconfirmemail")]
        public async Task SendConfirmEmailAsync(LoginModel.LoginIn model)
        {
            await _userService.SendConfirmEmailAgainAsync(model);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("changepassword")]
        public async Task ChangePasswordAsync(ChangePasswordModel model)
        {
            await _userService.ChangePasswordAsync(model);
        }
    }
}

