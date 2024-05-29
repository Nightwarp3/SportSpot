using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public interface IRotationService
    {
        Task<bool> DeleteRotation(Guid rotationId, Guid teamId);
        Task<Rotation?> GetRotation(Guid rotationId, Guid teamId);
        Task<IEnumerable<Rotation>> GetRotationsByTeam(Guid teamId);
        Task<Rotation?> UpsertRotation(Rotation rotation);
    }
}
