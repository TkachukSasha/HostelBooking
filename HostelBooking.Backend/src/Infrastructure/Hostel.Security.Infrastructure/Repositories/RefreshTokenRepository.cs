using Hostel.Security.Domain.Entities;
using Hostel.Security.Domain.Repositories;
using Hostel.Security.Infrastructure.Dal;
using Microsoft.EntityFrameworkCore;

namespace Hostel.Security.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository, IDisposable
    {
        private SecurityContext _context;
        private bool _disposed;

        public RefreshTokenRepository(SecurityContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            try
            {
                var existingToken = await _context.RefreshToken.FirstOrDefaultAsync(x => x.Token == token);
                return existingToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var refreshToken = await _context.RefreshToken.FirstOrDefaultAsync(x => x.RefreshTokenId == id);
                _context.RefreshToken.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreateAsync(RefreshToken refreshToken)
        {
            try
            {
                await _context.RefreshToken.AddAsync(refreshToken);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAllAsync(int userId)
        {
            IEnumerable<RefreshToken> refreshTokens = await _context.RefreshToken
                .Where(t => t.UserId == userId)
                .ToListAsync();

            _context.RefreshToken.RemoveRange(refreshTokens);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            _ = disposing;

            if (!_disposed)
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

            _disposed = true;
        }
    }
}
