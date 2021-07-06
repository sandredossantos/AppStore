using AppStore.Api.Language;
using AppStore.Api.Mapper;
using AppStore.Api.Models;
using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IApplicationMapper _applicationMapper;
        private readonly ILogger _logger;

        public ApplicationController
            (
            IApplicationService applicationService,
            IApplicationMapper applicationMapper,
            ILogger<ApplicationController> logger
            )
        {
            _applicationService = applicationService;
            _applicationMapper = applicationMapper;
            _logger = logger;
        }

        [HttpGet("GetAllApps")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllApps()
        {
            try
            {
                List<Application> applications = await _applicationService.GetAllApps();

                List<object> applicationsResponse = new List<object>();

                foreach (Application application in applications)
                {
                    applicationsResponse.Add(
                        new
                        {
                            Name = application.Name,
                            Code = application.Code,
                            Value = application.Value
                        });
                }

                return Ok(new { Success = true, Message = applicationsResponse });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { Success = false, Message = AppStoreMsg.INF0003 });
            }
        }

        [HttpPost("RegisterApp")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterApp(ApplicationModel applicationViewModel)
        {
            try
            {
                TryValidateModel(applicationViewModel);

                if (!ModelState.IsValid)
                    throw new Exception(AppStoreMsg.INF0008);

                Application application = _applicationMapper.ModelToEntity(applicationViewModel);

                await _applicationService.RegisterApp(application);

                return Ok(new { Success = true, Message = AppStoreMsg.INF0004 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}