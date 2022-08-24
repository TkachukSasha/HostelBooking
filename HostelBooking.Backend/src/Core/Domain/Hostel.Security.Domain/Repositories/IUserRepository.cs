using Hostel.Security.Domain.Entities;

namespace Hostel.Security.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(int userId);
        Task AddAsync(User user);
    }
}
