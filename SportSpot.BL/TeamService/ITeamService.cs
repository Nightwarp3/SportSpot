using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public interface ITeamService
    {
        public Team CreateTeam(Team team);
        public Team GetTeam(string teamPassword);
    }
}
