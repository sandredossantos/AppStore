using AppStore.Api.Models;
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

        public AppStoreController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("GetAllApps")]
        public async Task<IActionResult> GetAllApps()
        {
            try
            {
                List<Application> allApps = await _applicationService.GetAllApps();

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

            Application application = new Application();
            application.Code = Guid.NewGuid().ToString();
            application.Name = applicationViewModel.Name;
            application.Value = applicationViewModel.Value;

            application = await _applicationService.RegisterApplication(application);

            return Ok(application);
        }

        [HttpPost("BuyApp")]
        public IActionResult BuyApp()
        {
            _applicationService.BuyApp();

            return Ok();
        }
    }
}