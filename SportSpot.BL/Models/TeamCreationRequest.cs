namespace SportSpot.BL.Models
{
    public class TeamCreationRequest
    {
        public Team Team { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
