using Chat.Core.Application.Abtrasctions.Repositories;
using Chat.Core.Application.Abtrasctions.Services;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var user = await userService.GetByIdAsync(id);
            return Ok(user);
        }
    }
}
