using System.Text.Json.Serialization;

namespace SportSpot.BL.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
