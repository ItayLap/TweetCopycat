using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TweetCopycat.Models;

namespace TweetCopycat.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        public AuthController(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel model)
        {
            var result = await _userManager.CreateAsync(model, "DefaultPassword123!");
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("User registered successfully");
        }
        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok("Login logic here");
        }

    }
}
