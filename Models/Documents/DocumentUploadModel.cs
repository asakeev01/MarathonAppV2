using System;
using System.ComponentModel.DataAnnotations;
using MarathonApp.DAL.Enums;
using Microsoft.AspNetCore.Http;

namespace MarathonApp.Models.Documents
{
    public class DocumentUploadModel
    {
        [Required]
        public ImagesEnum Image { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}

