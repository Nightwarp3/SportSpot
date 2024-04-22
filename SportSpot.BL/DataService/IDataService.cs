using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public interface IDataService
    {
        public Team GetTeam(string guid);
        public IEnumerable<TeamUser> GetTeams();
    }
}
