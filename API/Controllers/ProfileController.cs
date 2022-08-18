﻿using System;
using MarathonApp.BLL.Services;
using MarathonApp.Models.Profiles;
using MarathonApp.Models.Users;
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
        public async Task CreateProfileAsync(ProfileCreateModel model)
        {
            await _profileService.CreateProfileAsync(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ObjectResult> GetProfileAsync()
        {
            var result = await _profileService.GetProfileAsync();
            return Ok(result);
        }

        //FOR ADMINS AND OWNER

        [HttpGet("profiles")]
        [Authorize(Roles = UserRolesModel.Admin + "," + UserRolesModel.Owner)]
        public async Task<ObjectResult> GetProfilesAsync()
        {
            var result = await _profileService.GetProfilesAsync();
            return Ok(result);
        }

        [HttpGet("{email}")]
        [Authorize(Roles = UserRolesModel.Admin + "," + UserRolesModel.Owner)]
        public async Task<ActionResult> GetProfileAsAdminAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return NotFound();

            var result = await _profileService.GetProfileAsAdminAsync(email);
            return Ok(result);
        }

        [HttpPut("{email}")]
        [Authorize(Roles = UserRolesModel.Admin + "," + UserRolesModel.Owner)]
        public async Task UpdateProfileAsync(ProfileDetailModel model)
        {
            await _profileService.UpdateProfileAsync(model);

        }

    }
}

