using AppStore.Api.Language;
using AppStore.Api.Mapper;
using AppStore.Api.Models;
using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AppStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IUserService _userService;
        private readonly IApplicationService _applicationService;
        private readonly IPurchaseMapper _purchaseMapper;
        private readonly ILogger _logger;
        public PurchaseController
            (
            IPurchaseService purchaseService,
            IUserService userService,
            IApplicationService applicationService,
            IPurchaseMapper purchaseMapper,
            ILogger<PurchaseController> logger
            )
        {
            _purchaseService = purchaseService;
            _userService = userService;
            _applicationService = applicationService;
            _purchaseMapper = purchaseMapper;
            _logger = logger;
        }

        [HttpPost("PurchaseApp")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PurchaseApp(PurchaseModel purchaseModel)
        {
            try
            {
                TryValidateModel(purchaseModel);

                if (!ModelState.IsValid)
                    throw new Exception(AppStoreMsg.INF0008);

                if (_userService.GetByTaxNumber(purchaseModel.TaxNumber).Result == null)
                    throw new Exception(string.Format(AppStoreMsg.INF0009, purchaseModel.TaxNumber));

                if (_applicationService.GetByCode(purchaseModel.Code).Result == null)
                    throw new Exception(string.Format(AppStoreMsg.INF0010, purchaseModel.Code));

                Purchase purchase = _purchaseMapper.ModelToEntity(purchaseModel);

                await _purchaseService.CreatePurchaseOrder(purchase);

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