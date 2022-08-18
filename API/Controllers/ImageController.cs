using System;
using MarathonApp.BLL.Services;
using MarathonApp.Models.Documents;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Documents;

namespace MarathonApp.API.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private IDocumentService _documentService;

        public ImageController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ObjectResult> GetDocumentAsync()
        {
            var result = await _documentService.GetDocumentAsync();

            return Ok(result);
        }

        [HttpPut("upload")]
        public async Task UploadDocumentAsync([FromForm]DocumentUploadModel model)
        {
            await _documentService.UploadDocumentAsync(model);
        }

        //FOR ADMINS AND OWNER

        [HttpPut("uploadasadmin")]
        [Authorize(Roles = UserRolesModel.Admin + "," + UserRolesModel.Owner)]
        public async Task UplaodDocumentAsAdminAsync([FromForm]DocumentUploadAsAdminModel model)
        {
            await _documentService.UploadDocumentAsAdminAsync(model);
        }
    }
}

