using System;
using System.ComponentModel.DataAnnotations;
using MarathonApp.DAL.Enums;
using Microsoft.AspNetCore.Http;

namespace Models.Documents
{
    public class DocumentUploadAsAdminModel
    {
        [StringLength(50)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public ImagesEnum Image { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}

