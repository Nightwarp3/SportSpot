using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportSpot.BL.Services;

namespace SportSpot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataService _dataService;

        public UserController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet, Route("valid")]
        public async Task<IActionResult> ValidateUser(string username)
        {
            try
            {
                var teamUsers = await _dataService.GetTeamUsers();
                return Ok(!teamUsers.Any(x => x.Username == username));
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while validating user.");
            }
        }

    }
}
