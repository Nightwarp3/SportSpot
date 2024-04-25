namespace SportSpot.BL.Models
{
    public class TeamUser
    {
        public TeamUser(Guid teamId, string passwordHash)
        {
            TeamId = teamId;
            PasswordHash = passwordHash;
        }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Guid TeamId { get; set; }
        public bool IsAdmin()
        {
            return !string.IsNullOrEmpty(Username);
        }
    }
}
