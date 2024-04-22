namespace SportSpot.BL.Models
{
    public class TeamUser
    {
        public string PasswordHash { get; set; }
        public Guid TeamId { get; set; }
    }
}
