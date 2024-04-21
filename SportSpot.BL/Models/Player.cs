using System.Text.Json.Serialization;

namespace SportSpot.BL.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public Team Team { get; set; }
    }
}
