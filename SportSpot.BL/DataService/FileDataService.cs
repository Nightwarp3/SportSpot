using SportSpot.BL.Models;
using System.Text.Json;

namespace SportSpot.BL.Services
{
    public class FileDataService : IDataService
    {
        private readonly string DataLocation = "./StoredData";
        private readonly string TeamUser = "TeamUser.json";

        public Team GetTeam(string guid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TeamUser> GetTeams()
        {
            string teamUsersJson = File.ReadAllText(Path.Combine(DataLocation, TeamUser));

            return JsonSerializer.Deserialize<IEnumerable<TeamUser>>(teamUsersJson);
        }
    }
}
