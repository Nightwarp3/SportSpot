using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public interface ISubstitutionService
    {
        Task<bool> DeleteSubstitution(Guid substitutionId, Guid gameId, Guid teamId);
        Task<Substitution?> GetSubstitution(Guid substitutionId, Guid gameId, Guid teamId);
        Task<IEnumerable<Substitution>> GetSubstitutionsByGame(Guid gameId, Guid teamId);
        Task<Substitution?> UpsertSubstitution(Substitution substitution, Guid gameId);
    }
}
