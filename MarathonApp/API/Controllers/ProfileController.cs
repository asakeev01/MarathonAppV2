using System;
using MarathonApp.BLL.Services;
using MarathonApp.DAL.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarathonApp.API.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPut("register")]
        [Authorize(Policy = "NewUserPolicy")]
        public async Task<ObjectResult> UpdateProfileAsync(ProfileViewModel model)
        {
            var result = await _profileService.UpdateProfileAsync(model);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}

