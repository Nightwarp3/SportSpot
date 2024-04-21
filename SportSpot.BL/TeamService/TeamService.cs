using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public class TeamService : ITeamService
    {
        public Team CreateTeam(Team team)
        {
            throw new NotImplementedException();
        }

        public Team GetTeam(string teamPassword)
        {
            var team = new Team
            {
                Id = Guid.NewGuid(),
                Description = "3/4 Grade Nibley City Soccer - 2024",
                Name = "Sharkicks",
                Positions = new List<Position>
                {
                    new Position { Id = Guid.NewGuid(), Name = "Goalie", NumberAllowed = 1 },
                    new Position { Id = Guid.NewGuid(), Name = "Defender", NumberAllowed = 2 },
                    new Position { Id = Guid.NewGuid(), Name = "Wildcard", NumberAllowed = 1 },
                    new Position { Id = Guid.NewGuid(), Name = "Midfield", NumberAllowed = 2 },
                    new Position { Id = Guid.NewGuid(), Name = "Forward", NumberAllowed = 2 },
                    new Position { Id = Guid.NewGuid(), Name = "Sub", NumberAllowed = null }
                },
                Rotations = new List<Rotation>
                {
                    new Rotation { Id = Guid.NewGuid(), Name = "Q1 - 1" },
                    new Rotation { Id = Guid.NewGuid(), Name = "Q1 - 2" },
                    new Rotation { Id = Guid.NewGuid(), Name = "Q2 - 1" },
                    new Rotation { Id = Guid.NewGuid(), Name = "Q2 - 2" },
                    new Rotation { Id = Guid.NewGuid(), Name = "Q3 - 1" },
                    new Rotation { Id = Guid.NewGuid(), Name = "Q3 - 2" },
                    new Rotation { Id = Guid.NewGuid(), Name = "Q4 - 1" },
                    new Rotation { Id = Guid.NewGuid(), Name = "Q4 - 2" }
                },
            };

            team.Players = new List<Player>
            {
                new Player { Id = Guid.NewGuid(), Team = team, Name = "Garrett" },
                new Player { Id = Guid.NewGuid(), Team = team, Name = "Erik" },
                new Player { Id = Guid.NewGuid(), Team = team, Name = "June" },
                new Player { Id = Guid.NewGuid(), Team = team, Name = "Brynlee" },
                new Player { Id = Guid.NewGuid(), Team = team, Name = "Owen" },
                new Player { Id = Guid.NewGuid(), Team = team, Name = "Evan" },
                new Player { Id = Guid.NewGuid(), Team = team, Name = "Daphnie" },
                new Player { Id = Guid.NewGuid(), Team = team, Name = "Brooklyn" },
                new Player { Id = Guid.NewGuid(), Team = team, Name = "Jaxson" },
                new Player { Id = Guid.NewGuid(), Team = team, Name = "Mason" },
                new Player { Id = Guid.NewGuid(), Team = team, Name = "Hugo" },
                new Player { Id = Guid.NewGuid(), Team = team, Name = "Ethan" }
            };

            return team;
        }
    }
}
