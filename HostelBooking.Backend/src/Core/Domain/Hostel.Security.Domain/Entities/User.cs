using Hostel.Security.Domain.ValueObjects;
using Hostel.Shared.Types;

namespace Hostel.Security.Domain.Entities
{
    public sealed class User : BaseEntity, IBaseEntity
    {
        public User()
        {
            RefreshToken = new HashSet<RefreshToken>();
        }

        public User(string email,
                    string password,
                    string role,
                    DateTime createdAt,
                    bool isDeleted)
        {
            Email = email;
            Password = password;
            Role = role;
            CreatedAt = createdAt;
            IsDeleted = isDeleted;
        }

        public int UserId { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public Role Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool? IsDeleted { get; set; }

        public ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
