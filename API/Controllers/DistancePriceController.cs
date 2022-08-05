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
    [Route("api/distancePrice")]
    public class DistancesPriceController : ControllerBase
    {
        private IDistancePriceService _distancePriceService;

        public DistancesPriceController(IDistancePriceService distancePriceService)
        {
            _distancePriceService = distancePriceService;
        }

        /// <summary>
        /// Delete Distance
        /// </summary>
        /// <param name="id">DistancPrice Id</param>
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
            await _distancePriceService.Delete(id);
        }
    }
}
