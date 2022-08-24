using Hostel.Shared.Types;

namespace Hostel.Security.Domain.Entities
{
    public sealed class RefreshToken : BaseEntity
    {
        public RefreshToken(string token,
                            int userId)
        {
            Token = token;
            UserId = userId;
        }

        public int RefreshTokenId { get; set; }
        public string Token { get; private set; }
        public int UserId { get; private set; }

        public User User { get; private set; }
    }
}
