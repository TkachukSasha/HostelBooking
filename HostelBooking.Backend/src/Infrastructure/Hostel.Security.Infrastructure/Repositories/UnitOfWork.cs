using Hostel.Security.Domain.Repositories;
using Hostel.Security.Infrastructure.Dal;

namespace Hostel.Security.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SecurityContext _context;

        public UnitOfWork(SecurityContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
    }
}
