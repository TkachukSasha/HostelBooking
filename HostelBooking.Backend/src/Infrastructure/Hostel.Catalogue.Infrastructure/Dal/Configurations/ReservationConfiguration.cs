using Hostel.Catalogue.Domain.Entities;
using Hostel.Catalogue.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostel.Catalogue.Infrastructure.Dal.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(x => x.ReservationId);

            builder.Property(x => x.DateFrom)
                   .HasConversion(x => x.Value, x => new Date(x))
                   .IsRequired();

            builder.Property(x => x.DateTo)
                   .HasConversion(x => x.Value, x => new Date(x))
                   .IsRequired();

            builder.Property(x => x.Capacity)
                   .HasConversion(x => x.Value, x => new Capacity(x))
                   .IsRequired();

            builder.Property(x => x.IsDeleted)
                   .HasColumnName("IsDeleted");

            builder.HasOne(x => x.Room)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey(x => x.RoomId);
        }
    }
}
