using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SportSpot.BL.Services;

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

        [Authorize(AuthenticationSchemes = NegotiateDefaults.AuthenticationScheme)]
        [HttpPost, Route("")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var jwtToken = _authService.AuthorizeUser(loginRequest.Email, loginRequest.Password);
                return Ok(jwtToken);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
    }
}
