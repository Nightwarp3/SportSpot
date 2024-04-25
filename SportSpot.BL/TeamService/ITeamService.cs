using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public interface ITeamService
    {
        Task<Team> CreateTeam(Team team);
        Task<bool> DeleteTeam(Team team);
        Task<Team?> GetTeam(Guid teamId);
        Task<Team?> UpdateTeam(Team team);
    }
}
