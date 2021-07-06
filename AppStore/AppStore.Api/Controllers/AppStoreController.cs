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
    public class AppStoreController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IPurchaseService _purchaseService;
        private readonly IUserService _userService;
        private readonly IApplicationMapper _applicationMapper;
        private readonly IPurchaseMapper _purchaseMapper;
        private readonly ILogger _logger;

        public AppStoreController(
            IApplicationService applicationService,
            IPurchaseService purchaseService,
            IUserService userService,
            IApplicationMapper applicationMapper,
            IPurchaseMapper purchaseMapper,
            ILogger<AppStoreController> logger
            )
        {
            _applicationService = applicationService;
            _purchaseService = purchaseService;
            _userService = userService;
            _applicationMapper = applicationMapper;
            _purchaseMapper = purchaseMapper;
            _logger = logger;
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
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { Success = false, Message = ex.Message });
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

                if (_userService.GetByTaxNumber(purchaseModel.TaxNumber).Result == null)
                    throw new Exception(string.Format(AppStoreMsg.INF0009, purchaseModel.TaxNumber));

                Purchase purchase = _purchaseMapper.ModelToEntity(purchaseModel);

                _purchaseService.CreatePurchaseOrder(purchase);

                return Ok(new { Success = true, Message = AppStoreMsg.INF0006 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}