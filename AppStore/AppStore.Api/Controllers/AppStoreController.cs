using AppStore.Api.Mapper;
using AppStore.Api.Models.JsonInput;
using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly IPurchaseMapper _purchaseMapper;
        private readonly IPurchaseService _purchaseService;

        public AppStoreController(
            IApplicationService applicationService,
            IApplicationMapper applicationMapper,
            IPurchaseMapper purchaseMapper,
            IPurchaseService purchaseService)
        {
            _applicationService = applicationService;
            _applicationMapper = applicationMapper;
            _purchaseMapper = purchaseMapper;
            _purchaseService = purchaseService;
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

        [HttpPost("RegisterApp")]
        public async Task<IActionResult> RegisterApp(ApplicationViewModel applicationViewModel)
        {
            TryValidateModel(applicationViewModel);

            if (!ModelState.IsValid) return BadRequest();

            Application application = _applicationMapper.ModelToEntity(applicationViewModel);

            await _applicationService.RegisterApplication(application);

            return Ok(new { Success = true, Message = "the application has been registered" });
        }

        [HttpPost("PurchaseApp")]
        public IActionResult PurchaseApp(PurchaseModel purchaseModel)
        {
            TryValidateModel(purchaseModel);

            if (!ModelState.IsValid) return BadRequest();

            Purchase purchase = _purchaseMapper.ModelToEntity(purchaseModel);

            _purchaseService.CreatePurchaseOrder(purchase);            

            return Ok(new { Success = true, Message = "the application has been registered" });
        }

        //[HttpGet("GetMyApps")]
        //public async Task<IActionResult> GetMyApps()
        //{
        //    try
        //    {
        //        return Ok();
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}