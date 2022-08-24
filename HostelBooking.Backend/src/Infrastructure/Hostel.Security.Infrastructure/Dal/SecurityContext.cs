using Hostel.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hostel.Security.Infrastructure.Dal
{
    public class SecurityContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
