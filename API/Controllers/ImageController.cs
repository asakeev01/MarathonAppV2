using System;
using MarathonApp.BLL.Services;
using MarathonApp.Models.Images;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Images;

namespace MarathonApp.API.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private IImagesService _imagesService;

        public ImageController(IImagesService imagesService)
        {
            _imagesService = imagesService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ObjectResult> GetImagesAsync()
        {
            var result = await _imagesService.GetImagesAsync();

            return Ok(result);
        }

        [HttpPut("upload")]
        public async Task<ObjectResult> UploadImageAsync([FromForm]ImageTypeViewModel model)
        {
            var result = await _imagesService.UploadImageAsync(model);
            if (result.IsSuccess)
                 return Ok(result);
            return BadRequest(result);
        }

        //FOR ADMINS AND OWNER

        [HttpPut("uploadasadmin")]
        [Authorize(Roles = UserRolesModel.Admin + "," + UserRolesModel.Owner)]
        public async Task<ObjectResult> UplaodImageAsAdminAsync([FromForm]ImageTypeIdViewModel model)
        {
            var result = await _imagesService.UploadImageAsAdminAsync(model);

            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}

