using System;
using API.Extensions;
using MarathonApp.BLL.Services;
using MarathonApp.Models.Partners;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.SavedFiles;

namespace MarathonApp.API.Controllers
{
    [ApiController]
    [Route("api/partners")]
    public class PartnersController : ControllerBase
    {
        private IPartnerService _partnerService;

        public PartnersController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Add([FromForm] SavedFileModel.Add<IFormFile> file)
        {
            await _partnerService.Add(file);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<PartnerModel.ListPartner>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<PartnerModel.ListPartner>> List()
        {
            return await _partnerService.List();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PartnerModel.Get), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<PartnerModel.Get> ById(int id)
        {
            return await _partnerService.ById(id);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Edit([FromForm] int id, [FromForm] SavedFileModel.Add<IFormFile> file)
        {
            await _partnerService.Edit(id, file);
        }
    }
}
