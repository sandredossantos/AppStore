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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;
        private readonly ILogger _logger;

        public UserController(
            IUserService userService, 
            IUserMapper userMapper,
            ILogger<UserController> logger
            )
        {
            _userService = userService;
            _userMapper = userMapper;
            _logger = logger;
        }
        
        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(UserModel userViewModel)
        {
            try
            {
                TryValidateModel(userViewModel);

                if (!ModelState.IsValid)
                    throw new Exception(AppStoreMsg.INF0008);

                User user = _userMapper.ModelToEntity(userViewModel);

                await _userService.CreateUser(user);

                return Ok(new { Success = true, Message = AppStoreMsg.INF0001 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}