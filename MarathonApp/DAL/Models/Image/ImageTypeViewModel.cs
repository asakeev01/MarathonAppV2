using System;
using MarathonApp.DAL.Enums;

namespace MarathonApp.DAL.Models.Image
{
    public class ImageTypeViewModel
    {
        public ImagesEnum Image { get; set; }
        public IFormFile File { get; set; }
    }
}

