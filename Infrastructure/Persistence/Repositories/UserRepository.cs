using System;
using System.Linq.Expressions;
using Domain.Common.Constants;
using Domain.Common.Contracts;
using Domain.Common.Helpers;
using Domain.Common.Resources;
using Domain.Entities.Users;
using Domain.Entities.Users.Constants;
using Domain.Entities.Users.Exceptions;
using Google.Apis.Util;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private UserManager<User> _userManager;
        private AppDbContext _repositoryContext;
        private IStringLocalizer<SharedResource> _localizer;

        public UserRepository(UserManager<User> userManager, AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
        {
            _userManager = userManager;
            _repositoryContext = repositoryContext;
            _localizer = localizer;
        }

        public async Task<bool> IsUserExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
           
            if (user != null)
                return true;
            return false;
        }

        public async Task CreateUserAsync(User user, string password)
        {
            if(await _userManager.FindByEmailAsync(user.Email) != null)
                throw new UserAlreadyExistsException(_localizer);
            await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetByEmailAsync(string? email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new UserDoesNotExistException(_localizer);

            return user;
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UserDoesNotExistException(_localizer);
            return user;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            if (user.EmailConfirmed)
                throw new EmailAlreadyConfirmedException(_localizer);

            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return emailToken;
        }

        public async Task ConfirmEmailAsync(User user, string emailToken)
        {
            if (user.EmailConfirmed)
                throw new EmailAlreadyConfirmedException(_localizer);

            var result = await _userManager.ConfirmEmailAsync(user, emailToken);

            if (!result.Succeeded)
                throw new InvalidTokenException(_localizer);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            var passwordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            return passwordToken;
        }

        public async Task ResetPasswordAsync(User user, string passwordToken, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, passwordToken, newPassword);

            if (!result.Succeeded)
                throw new InvalidTokenException(_localizer);
        }

        public async Task AddToRoleAsync(User user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
                throw new WrongRoleException(_localizer);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        public async Task CheckPasswordAsync(User user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);


            if (!result)
                throw new WrongPasswordException(_localizer);
        }

        public async Task ChangePasswordAsync(User user, string password, string newPassword)
        {
            await _userManager.ChangePasswordAsync(user, password, newPassword);
        }

        public async Task UpdateAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task<byte[]> GenerateExcel(IQueryable<User> users)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Users");

            var worksheet = excel.Workbook.Worksheets["Users"];

            for (int j = 0; j < AppConstants.UsersExcelColumns.Count; j++)
            {
                worksheet.Cells[AppConstants.UsersExcelColumns[j].Item1].Value = AppConstants.UsersExcelColumns[j].Item2;
            }

            worksheet.Cells["A1:O1"].Style.Font.Bold = true;
            worksheet.Cells["A1:O1"].Style.Font.Size = 14;
            int i = 2;
            foreach (var user in users)
            {
                if (user.Applications.Count > 0)
                {
                    foreach (var application in user.Applications)
                    {
                        worksheet.Cells[$"A{i}"].Value = user.Id;
                        worksheet.Cells[$"B{i}"].Value = user.Name + " " + user.Surname;
                        worksheet.Cells[$"C{i}"].Value = user.Email;
                        worksheet.Cells[$"D{i}"].Value = user.PhoneNumber;
                        var document = user.Documents.Where(x => x.IsArchived == false).FirstOrDefault();
                        worksheet.Cells[$"E{i}"].Value = document.GetString();
                        worksheet.Cells[$"F{i}"].Value = user.DateOfBirth == null ? "" : user.DateOfBirth.Value.ToString("dd/MM/yyyy");
                        worksheet.Cells[$"G{i}"].Value = user.Gender == true ? "Муж" : "Жен";
                        worksheet.Cells[$"H{i}"].Value = user.Tshirt == null ? "" : user.Tshirt.Value;
                        worksheet.Cells[$"I{i}"].Value = user.ExtraPhoneNumber;
                        worksheet.Cells[$"J{i}"].Value = user.GetAge(application.Marathon.Date);
                        worksheet.Cells[$"K{i}"].Value = application.Number;
                        worksheet.Cells[$"L{i}"].Value = application.Marathon.Date.ToString("dd/MM/yyyy");
                        worksheet.Cells[$"M{i}"].Value = application.Marathon.MarathonTranslations.Where(x => x.LanguageId == 2).First().Name;
                        worksheet.Cells[$"N{i}"].Value = application.DistanceAgeId == null ? "ЛОВЗ" : $"{application.DistanceAge.AgeFrom}-{application.DistanceAge.AgeTo}";
                        i += 1;
                    }
                }
                else
                {
                    worksheet.Cells[$"A{i}"].Value = user.Id;
                    worksheet.Cells[$"B{i}"].Value = user.Name + " " + user.Surname;
                    worksheet.Cells[$"C{i}"].Value = user.Email;
                    worksheet.Cells[$"D{i}"].Value = user.PhoneNumber;
                    var document = user.Documents.Where(x => x.IsArchived == false).FirstOrDefault();
                    worksheet.Cells[$"E{i}"].Value = document.GetString();
                    worksheet.Cells[$"F{i}"].Value = user.DateOfBirth == null ? "" : user.DateOfBirth.Value.ToString("dd/MM/yyyy");
                    worksheet.Cells[$"G{i}"].Value = user.Gender == true ? "Муж" : "Жен";
                    worksheet.Cells[$"H{i}"].Value = user.Tshirt == null ? "" : user.Tshirt.Value;
                    worksheet.Cells[$"I{i}"].Value = user.ExtraPhoneNumber;


                    i += 1;
                }
            }
            worksheet.Cells.AutoFitColumns();

            return excel.GetAsByteArray();
        }
    }
}

