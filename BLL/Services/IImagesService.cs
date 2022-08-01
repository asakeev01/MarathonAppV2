using System;
using System.Security.Claims;
using MarathonApp.DAL.EF;
using MarathonApp.DAL.Entities;
using MarathonApp.DAL.Enums;
using MarathonApp.Models.Images;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Images;

namespace MarathonApp.BLL.Services
{
    public interface IImagesService
    {
        Task<UserManagerResponse> UploadImageAsync(ImageTypeViewModel model);
        Task<ImageDetailViewModel> GetImagesAsync();
        Task<UserManagerResponse> UploadImageAsAdminAsync(ImageTypeIdViewModel model);
    }

    public class ImagesService : IImagesService
    {
        private MarathonContext _context;
        private IHttpContextAccessor _httpContext;
        private IWebHostEnvironment _webHostEnvironment;
        private UserManager<User> _userManager;

        public ImagesService(MarathonContext context, IHttpContextAccessor httpContext, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager)
        {
            _context = context;
            _httpContext = httpContext;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public async Task<UserManagerResponse> UploadImageAsync(ImageTypeViewModel model)
        {
            var file = model.File;
            if (file == null)
                return new UserManagerResponse
                {
                    Message = "There is no file",
                    IsSuccess = false
                };

            var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            var image = await _context.ImagesEntity.FirstOrDefaultAsync(i => i.UserId == userId.Value);
            

            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images/" + model.Image);
            string filePath = Path.Combine(directoryPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (model.Image == ImagesEnum.BackPassport)
                image.BackPassportPath = filePath;
            else if (model.Image == ImagesEnum.FrontPassport)
                image.FrontPassportPath = filePath;
            else if (model.Image == ImagesEnum.Insurance)
                image.InsurancePath = filePath;
            else
                image.DisabilityPath = filePath;

            await _context.SaveChangesAsync();

            return new UserManagerResponse
            {
                Message = "Image was successfully uploaded",
                IsSuccess = true
            };
        }

        public async Task<ImageDetailViewModel> GetImagesAsync()
        {
            var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var image = await _context.ImagesEntity.FirstOrDefaultAsync(i => i.UserId == userId.Value);
            var result = new ImageDetailViewModel
            {
                FrontPassportPath = image.FrontPassportPath,
                BackPassportPath = image.BackPassportPath,
                InsurancePath = image.InsurancePath,
                DisabilityPath = image.DisabilityPath
            };
            return result;
        }


        // FOR ADMINS AND OWNER


        public async Task<UserManagerResponse> UploadImageAsAdminAsync(ImageTypeIdViewModel model)
        {
            var file = model.File;
            if (file == null)
                return new UserManagerResponse
                {
                    Message = "There is no file",
                    IsSuccess = false
                };

            var email = model.Email;
            var user = await _userManager.FindByEmailAsync(email);
            var userId = user.Id;
            var image = await _context.ImagesEntity.FirstOrDefaultAsync(i => i.UserId == userId);

            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images/" + model.Image);
            string filePath = Path.Combine(directoryPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (model.Image == ImagesEnum.BackPassport)
                image.BackPassportPath = filePath;
            else if (model.Image == ImagesEnum.FrontPassport)
                image.FrontPassportPath = filePath;
            else if (model.Image == ImagesEnum.Insurance)
                image.InsurancePath = filePath;
            else
                image.DisabilityPath = filePath;

            await _context.SaveChangesAsync();

            return new UserManagerResponse
            {
                Message = "Image was successfully uploaded",
                IsSuccess = true
            };
        }
    }
}

