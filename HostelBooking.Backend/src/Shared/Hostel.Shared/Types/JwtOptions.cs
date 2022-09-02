namespace Hostel.Shared.Types
{
    public class JwtOptions
    {
        public string AccessTokenSecret { get; set; }
        public float AccessTokenExpirationMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string RefreshTokenSecret { get; set; }
        public float RefreshTokenExpirationMinutes { get; set; }
    }
}
