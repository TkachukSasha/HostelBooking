using Hostel.Catalogue.Infrastructure.Dal;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogueContext _context;

        public UnitOfWork(CatalogueContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
    }
}
