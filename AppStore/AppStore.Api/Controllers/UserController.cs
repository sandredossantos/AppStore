using AppStore.Api.Mapper;
using AppStore.Api.Models.JsonInput;
using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
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
        public async Task<IActionResult> CreateUser(UserViewModel userViewModel)
        {
            try
            {
                TryValidateModel(userViewModel);

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                User user = _userMapper.ModelToEntity(userViewModel);

                await _userService.CreateUser(user);

                return Ok(new { Success = true, Data = "user created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Data = ex.Message });
            }
        }
    }
}