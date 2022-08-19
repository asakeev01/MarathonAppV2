using System;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Applications;

namespace API.Controllers
{
    [ApiController]
    [Route("api/applications")]
    public class ApplicationController : ControllerBase
    {
        private IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("apply")]
        public async Task ApplyAsync(ApplyModel model)
        {
            _applicationService.ApplyAsync(model);
        }
    }
}

