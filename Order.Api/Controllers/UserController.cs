using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order.Data.Entities.IdentityEntities;
using Order.Service.HandleResponses;
using Order.Service.Services.UserService;
using Order.Service.Services.UserService.Dto;

namespace Order.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IUserService userService,
            UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto input)
        {
            var user = await _userService.Login(input);
            if (user == null)
                return Unauthorized(new CustomException(401));

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
            var user = await _userService.Register(input);
            if (user == null)
                return BadRequest(new CustomException(400, "Email Already Exists"));

            return Ok(user);
        }
    }
}
