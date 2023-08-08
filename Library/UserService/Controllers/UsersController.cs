using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Interface;

namespace UserService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetMyProfile()
        {
            var id = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
            var result = await _userService.GetById(id);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult<UserDTO>> Update(EditUserDTO newUserInfo)
        {
            var id = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
            var result = await _userService.UpdateProfile(id, newUserInfo);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("role")]
        public async Task<ActionResult<string>> ChangeRole(ChangeRoleDTO roleDTO)
        {
            await _userService.ChangeRole(roleDTO);
            return Ok(string.Format("Role on user: {0} is successfully changed to role {1}.", roleDTO.UserUsername));
        }
    }
}
