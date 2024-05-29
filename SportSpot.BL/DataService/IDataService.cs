using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public interface IDataService
    {
        Task<Team> CreateTeam(Team team);
        Task<bool> DeleteGame(Guid gameId, Guid teamId);
        Task<bool> DeletePlayer(Guid playerId, Guid teamId);
        Task<bool> DeletePosition(Guid positionId, Guid teamId);
        Task<bool> DeleteRotation(Guid rotationId, Guid teamId);
        Task<bool> DeleteSubstitution(Guid substitutionId, Guid gameId, Guid teamId);
        Task DeleteSubstitutionsByGame(Guid gameId, Guid teamId);
        Task DeleteSubstitutionsByPlayer(Guid playerId, Guid teamId);
        Task DeleteSubstitutionsByPosition(Guid positionId, Guid teamId);
        Task DeleteSubstitutionsByRotation(Guid rotationId, Guid teamId);
        Task<bool> DeleteTeam(Team team);
        Task<bool> DeleteTeamUser(TeamUser teamUser);
        Task<Game?> GetGame(Guid guid, Guid teamId);
        Task<IEnumerable<Game>> GetGamesByTeam(Guid teamId);
        Task<Player?> GetPlayer(Guid playerId, Guid teamId);
        Task<IEnumerable<Player>> GetPlayersByTeam(Guid teamId);
        Task<Position?> GetPosition(Guid positionId, Guid teamId);
        Task<IEnumerable<Position>> GetPositionsByTeam(Guid teamId);
        Task<Rotation?> GetRotation(Guid rotationId, Guid teamId);
        Task<IEnumerable<Rotation>> GetRotationsByTeam(Guid teamId);
        Task<Substitution?> GetSubstitution(Guid substitutionId, Guid gameId, Guid teamId);
        Task<IEnumerable<Substitution>> GetSubstitutionsByGame(Guid gameId, Guid teamId);
        Task<Team?> GetTeam(Guid guid);
        Task<TeamUser?> GetTeamUser(TeamUser user);
        Task<IEnumerable<TeamUser>> GetTeamUsers();
        Task<Game?> UpsertGame(Game game);
        Task<Player?> UpsertPlayer(Player player);
        Task<Position?> UpsertPosition(Position position);
        Task<Rotation?> UpsertRotation(Rotation rotation);
        Task<Substitution?> UpsertSubstitution(Substitution substitution, Guid teamId);
        Task<Team?> UpdateTeam(Team team);
        Task<bool> UpsertTeamUser(TeamUser teamUser);
    }
}
