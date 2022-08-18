using System;
using System.Net;
using System.Security.Claims;
using MarathonApp.DAL.EF;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Exceptions;
using MarathonApp.Models.Profiles;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace MarathonApp.BLL.Services
{
    public interface IProfileService
    {
        Task CreateProfileAsync(ProfileCreateModel model);
        Task<ProfileDetailModel> GetProfileAsync();
        // FOR ADMINS AND OWNER
        Task<IQueryable<ProfilesModel>> GetProfilesAsync();
        Task<ProfileDetailModel> GetProfileAsAdminAsync(string userId);
        Task UpdateProfileAsync(ProfileDetailModel model);
    }

    public class ProfileService : IProfileService
    {
        private UserManager<User> _userManager;
        private IConfiguration _configuration;
        //private IEmailService _emailService;
        private RoleManager<IdentityRole> _roleManager;
        private IHttpContextAccessor _httpContext;

        public ProfileService(UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _httpContext = httpContext;
        }

        public async Task CreateProfileAsync(ProfileCreateModel model)
        {
            var email = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email);
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

            if (!result.Succeeded)
                throw new HttpException("Ошибка во время создания профиля", HttpStatusCode.BadRequest);
        }

        public async Task<ProfileDetailModel> GetProfileAsync()
        {
            var email = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email);
            var identityUser = await _userManager.FindByEmailAsync(email.Value);
            var user = new ProfileDetailModel
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


        public async Task<IQueryable<ProfilesModel>> GetProfilesAsync()
        {
            var identityUsers = _userManager.Users;
            var users = identityUsers.Select(c => new ProfilesModel
            {
                Email = c.Email,
                Name = c.Name,
                Surname = c.Surname
            });
            return users;
        }

        public async Task<ProfileDetailModel> GetProfileAsAdminAsync(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser == null)
                throw new HttpException("There is no such user", HttpStatusCode.BadRequest);

            var user = new ProfileDetailModel
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

        public async Task UpdateProfileAsync(ProfileDetailModel model)
        {
            var identityUser = await _userManager.FindByEmailAsync(model.Email);
            if (identityUser == null)
                throw new HttpException("There is no such user", HttpStatusCode.BadRequest);

            identityUser.Name = model.Name;
            identityUser.Surname = model.Surname;
            identityUser.DateOfBirth = model.DateOfBirth;
            identityUser.Gender = model.Gender;
            identityUser.Tshirt = model.Tshirt;
            identityUser.Country = model.Country;
            identityUser.PhoneNumber = model.PhoneNumber;
            identityUser.ExtraPhoneNumber = model.ExtraPhoneNumber;

            var result = await _userManager.UpdateAsync(identityUser);

            if (!result.Succeeded)
                throw new HttpException("Profile was not updated", HttpStatusCode.BadRequest);
        }
    }
}

