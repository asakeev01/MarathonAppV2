using System;
using System.ComponentModel.DataAnnotations;
using MarathonApp.DAL.Enums;
using Microsoft.AspNetCore.Http;

namespace Models.Images
{
    public class ImageTypeIdViewModel
    {
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        public ImagesEnum Image { get; set; }
        public IFormFile File { get; set; }
    }
}

