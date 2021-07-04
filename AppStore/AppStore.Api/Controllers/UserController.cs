using AppStore.Api.Language;
using AppStore.Api.Mapper;
using AppStore.Api.Models;
using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserService userService, IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }
        
        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(UserViewModel userViewModel)
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
            catch (Exception)
            {
                return BadRequest(new { Success = false, Message = AppStoreMsg.INF0002 });
            }
        }
    }
}