namespace StokedSports.Mobile.Core.Models
{
    public class AuthenticationResponse
    {
        public string JwtToken { get; set; }
        public User User { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
