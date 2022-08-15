using System;
using MarathonApp.BLL.Services;
using MarathonApp.Models.Partners;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.SavedFiles;

namespace MarathonApp.API.Controllers
{
    [ApiController]
    [Route("api/marathons")]
    public class MarathonController : ControllerBase
    {
        private IMarathonService _marathonService;

        public MarathonController(IMarathonService marathonService)
        {
            _marathonService = marathonService;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Add(MarathonModel.AddMarathon model)
        {
            await _marathonService.Add(model);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<MarathonModel.ListMarathon>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<MarathonModel.ListMarathon>> List()
        {
            return await _marathonService.List();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MarathonModel.GetMarathon), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<MarathonModel.GetMarathon> ById(int id)
        {
            var result = await _marathonService.ById(id);
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
        public async Task Edit(MarathonModel.EditMarathon model)
        {
            await _marathonService.Edit(model);
        }

        [HttpPut]
        [Route("distance")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task EditDistance(MarathonModel.EditMarathonDistance model)
        {
            await _marathonService.EditDistance(model);
        }

        [HttpPost]
        [Route("partner")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task AddPartner(MarathonModel.AddPartner model)
        {
            await _marathonService.AddPartner(model);
        }

        [HttpDelete]
        [Route("partner")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task RemovePartner(MarathonModel.DeletePartner model)
        {
            await _marathonService.DeletePartner(model);
        }

        [HttpPost]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task AddImage([FromForm] int id, [FromForm] SavedFileModel.Add<IFormFile> file)
        {
            await _marathonService.AddImage(id, file);
        }

        [HttpDelete]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeleteImage(MarathonModel.DeleteImage model)
        {
            await _marathonService.DeleteImage(model);
        }
    }
}
