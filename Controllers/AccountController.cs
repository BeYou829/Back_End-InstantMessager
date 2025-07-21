using Chat.Core.Application.Abtrasctions.Services;
using Chat.Core.Application.DTOs.Login;
using Chat.Core.Application.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(IAccountService accountService, IUserService userService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var token = await accountService.Login(loginRequest);
            if (token.IsFailure)
                return BadRequest(new { ErrorCode = token.Error.Code, ErrorMessage = token.Error.Description });

            return Ok(new { token = token.Value });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SaveUserDTO saveUserDTO)
        {
            var token = await accountService.Register(saveUserDTO);

            if (token.IsFailure)
                return BadRequest(token.Error);

            return Ok(token.Value);
        }
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userInfo = await userService.GetByIdAsync(id);
            return Ok(userInfo);
        }
    }
}
