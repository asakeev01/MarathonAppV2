using System;
using MarathonApp.BLL.Services;
using MarathonApp.Models.Partners;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task Add(PartnerModel.AddPartner model)
        {
            await _partnerService.Add(model);
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
            var result = await _partnerService.ById(id);
            if (result == null)
            {
                Response.StatusCode = 404;
            }
            return result;
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Edit(PartnerModel.Edit model)
        {
            await _partnerService.Edit(model);
        }
    }
}
