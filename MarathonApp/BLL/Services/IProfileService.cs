using System;
using MarathonApp.DAL.Entities;
using MarathonApp.DAL.Models.User;
using Microsoft.AspNetCore.Identity;

namespace MarathonApp.BLL.Services
{
    public interface IProfileService
    {
        Task<UserManagerResponse> UpdateProfileAsync(ProfileViewModel model);
    }

    public class ProfileService : IProfileService
    {
        private UserManager<User> _userManager;
        private IConfiguration _configuration;
        private IEmailService _emailService;
        private RoleManager<IdentityRole> _roleManager;

        public ProfileService(UserManager<User> userManager, IConfiguration configuration, IEmailService emailService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public async Task<UserManagerResponse> UpdateProfileAsync(ProfileViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register form is empty");


            var identityUser = await _userManager.FindByEmailAsync(model.Email);

            identityUser.Email = model.Email;
            identityUser.UserName = model.Email;
            identityUser.Name = model.Name;
            identityUser.Surname = model.Surname;
            identityUser.DateOfBirth = model.DateOfBirth;
            identityUser.Gender = model.Gender;
            identityUser.Tshirt = model.Tshirt;
            identityUser.Country = model.Country;
            identityUser.PhoneNumber = model.PhoneNumber;
            identityUser.ExtraPhoneNumber = model.ExtraPhoneNumber;

            var result = await _userManager.UpdateAsync(identityUser);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Profile was successfully created!",
                    IsSuccess = true
                };
            }
            return new UserManagerResponse
            {
                Message = "Profile was not created",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}

