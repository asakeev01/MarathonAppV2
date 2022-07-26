using System;
using MarathonApp.BLL.Services;
using MarathonApp.DAL.Models.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut("upload")]
        public async Task<ObjectResult> UploadImageAsync([FromForm]ImageTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _imagesService.UploadImageAsync(model);

                if (result.IsSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpGet]
        [Authorize]
        public async Task<ObjectResult> GetImagesAsync()
        {
            var result = await _imagesService.GetImagesAsync();

            return Ok(result);
        }
    }
}

