using Microsoft.AspNetCore.Mvc;
using SportSpot.BL.Models;
using System.Text.Json;

namespace SportSpot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly AuthorizationService _authService;
        public AuthenticateController(AuthorizationService authService)
        {
            _authService = authService;
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var jwtToken = await _authService.AuthorizeUser(loginRequest.Username.ToLower(), loginRequest.Password);
                return Ok(JsonSerializer.Serialize(jwtToken));
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
    }
}
