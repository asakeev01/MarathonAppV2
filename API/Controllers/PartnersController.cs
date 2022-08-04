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

        /// <summary>
        /// Add partner
        /// </summary>
        /// <param name="image">Partner`s image</param>
        /// <response code="401">Authorization required</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Uncaught, unknown error</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Add([FromForm] SavedFileModel.Add<IFormFile> file)
        {
            await _partnerService.Add(file);
        }

        /// <summary>
        /// List of partners
        /// </summary>
        /// <response code="401">Authorization required</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Uncaught, unknown error</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<PartnerModel.ListPartner>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<PartnerModel.ListPartner>> List()
        {
            return await _partnerService.List();
        }

        /// <summary>
        /// Partner by Id
        /// </summary>
        /// <param name="id">Partner`s Id</param>
        /// <response code="401">Authorization required</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Partner not found</response>
        /// <response code="500">Uncaught, unknown error</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PartnerModel.Get), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<PartnerModel.Get> ById(int id)
        {
            return await _partnerService.ById(id);
        }

        /// <summary>
        /// Edit Partner
        /// </summary>
        /// <param name="id">Partner`s Id</param>
        /// <param file="image">Partner`s image</param>
        /// <response code="401">Authorization required</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Partner not found</response>
        /// <response code="500">Uncaught, unknown error</response>
        [HttpPut]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Edit([FromForm] int id, [FromForm] SavedFileModel.Add<IFormFile> file)
        {
            await _partnerService.Edit(id, file);
        }

        /// <summary>
        /// Delete Partner
        /// </summary>
        /// <param name="id">Partner`s Id</param>
        /// <response code="401">Authorization required</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Partner not found</response>
        /// <response code="500">Uncaught, unknown error</response>
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Delete(int id)
        {
            await _partnerService.Delete(id);
        }
    }
}
