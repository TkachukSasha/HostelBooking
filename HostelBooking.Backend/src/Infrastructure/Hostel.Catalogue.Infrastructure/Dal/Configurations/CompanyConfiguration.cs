using Hostel.Catalogue.Domain.Entities;
using Hostel.Catalogue.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostel.Catalogue.Infrastructure.Dal.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.CompanyId);

            builder.Property(x => x.Name)
                   .HasConversion(x => x.Value, x => new Name(x))
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasConversion(x => x.Value, x => new Description(x))
                   .IsRequired();

            builder.Property(x => x.City)
                   .HasConversion(x => x.Value, x => new City(x))
                   .IsRequired();

            builder.Property(x => x.IsDeleted)
                   .HasColumnName("IsDeleted");
        }
    }
}
