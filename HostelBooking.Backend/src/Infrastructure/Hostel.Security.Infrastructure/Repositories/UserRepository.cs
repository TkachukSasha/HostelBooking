using Hostel.Security.Domain.Entities;
using Hostel.Security.Domain.Repositories;
using Hostel.Security.Infrastructure.Dal;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Hostel.Security.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private SecurityContext _context;
        private readonly ILogger _logger;
        private bool _disposed;

        public UserRepository(SecurityContext context,
                              ILogger logger)
        {
            (_context, _logger) = (context, logger);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke get by email method");
                throw ex;
            }
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            try
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.UserId == userId);
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke get by id method");
                throw ex;
            }
        }

        public async Task AddAsync(User user)
        {
            try
            {
                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke add user method");
                throw ex;
            }
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
