using AppStore.Api.Language;
using AppStore.Api.Mapper;
using AppStore.Api.Models;
using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllApps()
        {
            try
            {
                List<Application> allApps = await _applicationService.GetAllApps();

                return Ok(allApps);
            }
            catch (Exception)
            {
                return BadRequest(new { Success = false, Message = AppStoreMsg.INF0003 });
            }
        }

        [HttpPost("RegisterApp")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterApp(ApplicationViewModel applicationViewModel)
        {
            try
            {
                TryValidateModel(applicationViewModel);

                if (!ModelState.IsValid)
                    throw new Exception(AppStoreMsg.INF0008);

                Application application = _applicationMapper.ModelToEntity(applicationViewModel); 

                await _applicationService.RegisterApplication(application);

                return Ok(new { Success = true, Message = AppStoreMsg.INF0004 });
            }
            catch(Exception)
            {
                return BadRequest(new { Success = false, Message = AppStoreMsg.INF0005 });
            }
        }

        [HttpPost("PurchaseApp")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PurchaseApp(PurchaseModel purchaseModel)
        {
            try
            {
                TryValidateModel(purchaseModel);

                if (!ModelState.IsValid)
                    throw new Exception(AppStoreMsg.INF0008);

                Purchase purchase = _purchaseMapper.ModelToEntity(purchaseModel);

                _purchaseService.CreatePurchaseOrder(purchase);

                return Ok(new { Success = true, Message = AppStoreMsg.INF0006 });
            }
            catch(Exception)
            {
                return BadRequest(new { Success = false, Message = AppStoreMsg.INF0007 });
            }
        }
    }
}