using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public class TeamService : ITeamService
    {
        private readonly IDataService _dataService;

        public TeamService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<Team> CreateTeam(Team team)
        {
            return await _dataService.CreateTeam(team);
        }

        public async Task<Team?> UpdateTeam(Team team)
        {
            var newTeam = await _dataService.UpdateTeam(team);

            return newTeam;
        }

        public async Task<bool> DeleteTeam(Team team)
        {
            return await _dataService.DeleteTeam(team);
        }

        public async Task<Team?> GetTeam(Guid teamId)
        {
            return await _dataService.GetTeam(teamId);
        //    var team = new Team
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "3/4 Grade Nibley City Soccer - 2024",
        //        Name = "Sharkicks",
        //        Positions = new List<Position>
        //        {
        //            new Position { Id = Guid.NewGuid(), Name = "Goalie", NumberAllowed = 1 },
        //            new Position { Id = Guid.NewGuid(), Name = "Defender", NumberAllowed = 2 },
        //            new Position { Id = Guid.NewGuid(), Name = "Wildcard", NumberAllowed = 1 },
        //            new Position { Id = Guid.NewGuid(), Name = "Midfield", NumberAllowed = 2 },
        //            new Position { Id = Guid.NewGuid(), Name = "Forward", NumberAllowed = 2 },
        //            new Position { Id = Guid.NewGuid(), Name = "Sub", NumberAllowed = null }
        //        },
        //        Rotations = new List<Rotation>
        //        {
        //            new Rotation { Id = Guid.NewGuid(), Name = "Q1 - 1" },
        //            new Rotation { Id = Guid.NewGuid(), Name = "Q1 - 2" },
        //            new Rotation { Id = Guid.NewGuid(), Name = "Q2 - 1" },
        //            new Rotation { Id = Guid.NewGuid(), Name = "Q2 - 2" },
        //            new Rotation { Id = Guid.NewGuid(), Name = "Q3 - 1" },
        //            new Rotation { Id = Guid.NewGuid(), Name = "Q3 - 2" },
        //            new Rotation { Id = Guid.NewGuid(), Name = "Q4 - 1" },
        //            new Rotation { Id = Guid.NewGuid(), Name = "Q4 - 2" }
        //        },
        //    };

        //    team.Players = new List<Player>
        //    {
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "Garrett" },
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "Erik" },
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "June" },
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "Brynlee" },
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "Owen" },
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "Evan" },
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "Daphnie" },
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "Brooklyn" },
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "Jaxson" },
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "Mason" },
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "Hugo" },
        //        new Player { Id = Guid.NewGuid(), TeamId = team.Id, Name = "Ethan" }
        //    };

        //    return team;
        }
    }
}
