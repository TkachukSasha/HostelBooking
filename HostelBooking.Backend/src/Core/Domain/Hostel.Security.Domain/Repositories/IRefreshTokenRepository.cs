using Hostel.Security.Domain.Entities;

namespace Hostel.Security.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByTokenAsync(string token);

        Task CreateAsync(RefreshToken refreshToken);

        Task DeleteAsync(int id);

        Task DeleteAllAsync(int userId);
    }
}
