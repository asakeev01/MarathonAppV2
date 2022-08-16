using System;
using MarathonApp.DAL.Enums;
using Microsoft.AspNetCore.Http;

namespace MarathonApp.Models.Images
{
    public class ImageTypeViewModel
    {
        public ImagesEnum Image { get; set; }
        public IFormFile File { get; set; }
    }
}

