using AppStore.Api.Mapper;
using AppStore.Api.Models.JsonInput;
using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppStoreController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IApplicationMapper _applicationMapper;

        public AppStoreController(IApplicationService applicationService, IApplicationMapper applicationMapper)
        {
            _applicationService = applicationService;
            _applicationMapper = applicationMapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Application> allApps = await _applicationService.GetAll();

                return Ok(allApps);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("RegisterApplication")]
        public async Task<IActionResult> RegisterApplication(ApplicationViewModel applicationViewModel)
        {
            TryValidateModel(applicationViewModel);

            if (!ModelState.IsValid) return BadRequest();

            Application application = _applicationMapper.ModelToEntity(applicationViewModel);

            await _applicationService.RegisterApplication(application);

            return Ok(new { Success = true, Data = "the application has been registered" });
        }

        [HttpPost("BuyApp")]
        public IActionResult BuyApp()
        {
            _applicationService.BuyApp();

            return Ok();
        }
    }
}