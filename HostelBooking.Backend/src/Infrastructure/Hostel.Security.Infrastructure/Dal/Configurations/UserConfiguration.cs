using Hostel.Security.Domain.Entities;
using Hostel.Security.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostel.Security.Infrastructure.Dal.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Email)
                   .HasConversion(x => x.Value, x => new Email(x))
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Password)
                   .HasConversion(x => x.Value, x => new Password(x))
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Role)
                   .HasConversion(x => x.Value, x => new Role(x))
                   .IsRequired();

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.IsDeleted)
                  .HasColumnName("IsDeleted");
        }
    }
}
