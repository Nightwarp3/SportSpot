using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public class RotationService : IRotationService
    {
        private readonly IDataService _dataService;

        public RotationService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<Rotation>> GetRotationsByTeam(Guid teamId)
        {
            return await _dataService.GetRotationsByTeam(teamId);
        }

        public async Task<Rotation?> GetRotation(Guid rotationId, Guid teamId)
        {
            return await _dataService.GetRotation(rotationId, teamId);
        }

        public async Task<Rotation?> UpsertRotation(Rotation rotation)
        {
            return await _dataService.UpsertRotation(rotation);
        }

        public async Task<bool> DeleteRotation(Guid rotationId, Guid teamId)
        {
            await _dataService.DeleteSubstitutionsByRotation(rotationId, teamId);
            return await _dataService.DeleteRotation(rotationId, teamId);
        }
    }
}
