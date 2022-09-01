using Hostel.Catalogue.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hostel.Catalogue.Infrastructure.Dal
{
    public class CatalogueContext : DbContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Room> Room { get; set; }

        public CatalogueContext(DbContextOptions<CatalogueContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
