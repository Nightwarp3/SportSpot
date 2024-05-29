using Microsoft.AspNetCore.Mvc;
using SportSpot.BL.Models;
using SportSpot.BL.Services;

namespace SportSpot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotationController : ControllerBase
    {
        private readonly AuthorizationService _authorizationService;
        private readonly IRotationService _rotationService;

        public RotationController(AuthorizationService authorizationService, IRotationService rotationService)
        {
            _authorizationService = authorizationService;
            _rotationService = rotationService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetRotation(Guid rotationGuid)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);

                var rotation = await _rotationService.GetRotation(rotationGuid, team.Id);

                return Ok(rotation);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting rotation. Request Guid: {requestGuid}, Endpoint: GetRotation, HTTPGet, Error: {ex.Message}");
            }
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> UpsertRotation([FromBody] Rotation rotation)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                rotation.TeamId = team.Id;

                var newRotation = await _rotationService.UpsertRotation(rotation);

                return Ok(newRotation);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error updating or inserting rotation. Request Guid: {requestGuid}, Endpoint: UpsertRotation, HTTPPost, Error: {ex.Message}");
            }
        }

        [HttpDelete, Route("")]
        public async Task<IActionResult> DeleteRotation([FromBody] Rotation rotation)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                rotation.TeamId = team.Id;

                var rotationDeleted = await _rotationService.DeleteRotation(rotation.Id, team.Id);

                return Ok(rotationDeleted);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error deleting rotation. Request Guid: {requestGuid}, Endpoint: DeleteRotation, HTTPDelete, Error: {ex.Message}");
            }
        }
    }
}
