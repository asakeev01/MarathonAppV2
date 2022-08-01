using System;
using System.Security.Claims;
using MarathonApp.DAL.EF;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Profiles;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace MarathonApp.BLL.Services
{
    public interface IProfileService
    {
        Task<UserManagerResponse> CreateProfileAsync(ProfileCreateViewModel model);
        Task<ProfileDetailViewModel> GetProfileAsync();
        // FOR ADMINS AND OWNER
        Task<IQueryable<ProfileViewModel>> GetProfilesAsync();
        Task<ProfileDetailViewModel> GetProfileAsAdminAsync(string userId);
        Task<UserManagerResponse> UpdateProfileAsync(ProfileDetailViewModel model);
    }

    public class ProfileService : IProfileService
    {
        private UserManager<User> _userManager;
        private IConfiguration _configuration;
        //private IEmailService _emailService;
        private RoleManager<IdentityRole> _roleManager;
        private IHttpContextAccessor _context;

        public ProfileService(UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IHttpContextAccessor context)
        {
            _userManager = userManager;
            _configuration = configuration;
            //_emailService = emailService;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<UserManagerResponse> CreateProfileAsync(ProfileCreateViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register form is empty");

            var email = _context.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email);
            var identityUser = await _userManager.FindByEmailAsync(email.Value);
            
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

        public async Task<ProfileDetailViewModel> GetProfileAsync()
        {
            var email = _context.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email);
            var identityUser = await _userManager.FindByEmailAsync(email.Value);
            var user = new ProfileDetailViewModel
            {
                Email = identityUser.Email,
                Name = identityUser.Name,
                Surname = identityUser.Surname,
                DateOfBirth = identityUser.DateOfBirth,
                Gender = identityUser.Gender,
                Tshirt = identityUser.Tshirt,
                Country = identityUser.Country,
                PhoneNumber = identityUser.PhoneNumber,
                ExtraPhoneNumber = identityUser.ExtraPhoneNumber
            };
            return user;
        }


        // FOR ADMINS AND OWNER


        public async Task<IQueryable<ProfileViewModel>> GetProfilesAsync()
        {
            var identityUsers = _userManager.Users;
            var users = identityUsers.Select(c => new ProfileViewModel
            {
                Email = c.Email,
                Name = c.Name,
                Surname = c.Surname
            });
            return users;
        }

        public async Task<ProfileDetailViewModel> GetProfileAsAdminAsync(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser == null)
            {
                throw new Exception("There is no such user");
            }

            var user = new ProfileDetailViewModel
            {
                Email = identityUser.Email,
                Name = identityUser.Name,
                Surname = identityUser.Surname,
                DateOfBirth = identityUser.DateOfBirth,
                Gender = identityUser.Gender,
                Tshirt = identityUser.Tshirt,
                Country = identityUser.Country,
                PhoneNumber = identityUser.PhoneNumber,
                ExtraPhoneNumber = identityUser.ExtraPhoneNumber
            };
            return user;
        }

        public async Task<UserManagerResponse> UpdateProfileAsync(ProfileDetailViewModel model)
        {
            var identityUser = await _userManager.FindByEmailAsync(model.Email);
            if (identityUser == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no such user",
                    IsSuccess = false
                };
            }

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
                    Message = "Profile was successfully changed!",
                    IsSuccess = true
                };
            }
            return new UserManagerResponse
            {
                Message = "Profile was not updated",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}

