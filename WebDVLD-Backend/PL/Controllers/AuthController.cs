using BLL.DTOs.Auth;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _AuthService;

        public AuthController(IAuthService AuthService)
        {
            _AuthService = AuthService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] clsLoginRequest Request)
        {
            var Result = await _AuthService.LoginAsync(Request);

            if (Result == null)
                return Unauthorized();

            return Ok(Result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] clsRefreshRequest Request)
        {
            var Result = await _AuthService.RefreshTokenAsync(Request);

            if (Result == null)
                return Unauthorized();

            return Ok(Result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] clsLogoutRequest Request)
        {
            bool IsLoggedOut =
                await _AuthService.LogoutAsync(Request);

            if (!IsLoggedOut)
                return Unauthorized();

            return NoContent();
        }
    }
}
