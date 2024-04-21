using System.Text.Json.Serialization;

namespace SportSpot.BL.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        [JsonIgnore]
        public Team Team { get; set; }
        public DateTime GameStart { get; set; }
        public IEnumerable<Player> AttendingPlayers { get; set; }
        public IEnumerable<Substitution> Substitutions { get; set; }
    }
}
