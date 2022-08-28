using Hostel.Catalogue.Domain.Entities;
using Hostel.Catalogue.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostel.Catalogue.Infrastructure.Dal.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(x => x.RoomId);

            builder.Property(x => x.Number)
                   .HasConversion(x => x.Value, x => new Number(x))
                   .IsRequired();

            builder.Property(x => x.Floor)
                   .HasConversion(x => x.Value, x => new Floor(x))
                   .IsRequired();

            builder.Property(x => x.Capacity)
                   .HasConversion(x => x.Value, x => new Capacity(x))
                   .IsRequired();

            builder.Property(x => x.IsDeleted)
                   .HasColumnName("IsDeleted");

            builder.HasOne(x => x.Company)
                   .WithMany(x => x.Rooms)
                   .HasForeignKey(x => x.CompanyId);
        }
    }
}
