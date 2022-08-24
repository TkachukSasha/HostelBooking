namespace Hostel.Security.Domain.Entities
{
    public sealed class AccessToken
    {
        public string Value { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
