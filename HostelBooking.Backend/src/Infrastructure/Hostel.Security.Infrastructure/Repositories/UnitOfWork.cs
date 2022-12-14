using Hostel.Security.Infrastructure.Dal;
using Hostel.Shared.Types;

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
