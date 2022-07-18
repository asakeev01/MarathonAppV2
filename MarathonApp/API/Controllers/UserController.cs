using System;
using MarathonApp.BLL.Services;
using MarathonApp.DAL.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarathonApp.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("registerowner")]
        public async Task<ObjectResult> RegisterOwnerAsync()
        {
            var result = await _userService.RegisterOwnerAsync();
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("registeradmin")]
        [Authorize(Roles = UserRolesModel.Owner)]
        public async Task<ObjectResult> RegisterAdminAsync(AdminOwnerRegisterModel model)
        {
            var result = await _userService.RegisterAdminAsync(model);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("register")]
        public async Task<ObjectResult> RegisterAsync(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpPost("login")]
        public async Task<ObjectResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(model);

                if (result.IsSuccess)
                    return Ok(result);
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpGet("confirmemail")]
        public async Task<ActionResult> ConfirmEmailAsync(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("forgetpassword")]
        public async Task<ActionResult> ForgetPasswordAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _userService.ForgetPasswordAsync(email);

            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("resetpassword")]
        public async Task<ActionResult> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }
    }
}

