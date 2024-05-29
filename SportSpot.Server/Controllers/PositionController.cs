using Microsoft.AspNetCore.Mvc;
using SportSpot.BL.Models;
using SportSpot.BL.Services;

namespace SportSpot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly AuthorizationService _authorizationService;
        private readonly IPositionService _positionService;

        public PositionController(AuthorizationService authorizationService, IPositionService positionService)
        {
            _authorizationService = authorizationService;
            _positionService = positionService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetPosition(Guid positionGuid)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);

                var position = await _positionService.GetPosition(positionGuid, team.Id);

                return Ok(position);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting position. Request Guid: {requestGuid}, Endpoint: GetPosition, HTTPGet, Error: {ex.Message}");
            }
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> UpsertPosition([FromBody] Position position)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                position.TeamId = team.Id;

                var newPosition = await _positionService.UpsertPosition(position);

                return Ok(newPosition);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error updating or inserting position. Request Guid: {requestGuid}, Endpoint: UpsertPosition, HTTPPost, Error: {ex.Message}");
            }
        }

        [HttpDelete, Route("")]
        public async Task<IActionResult> DeletePosition([FromBody] Position position)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                position.TeamId = team.Id;

                var positionDeleted = await _positionService.DeletePosition(position.Id, team.Id);

                return Ok(positionDeleted);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error deleting position. Request Guid: {requestGuid}, Endpoint: DeletePosition, HTTPDelete, Error: {ex.Message}");
            }
        }
    }
}
