using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Authenticate([FromBody] string password)
        {
            try
            {
                var jwtToken = _authService.AuthorizeUser(password);
                return Ok(jwtToken);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
    }
}
