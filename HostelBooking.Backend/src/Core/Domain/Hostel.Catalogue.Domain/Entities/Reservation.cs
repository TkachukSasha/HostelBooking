using Hostel.Catalogue.Domain.ValueObjects;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Domain.Entities
{
    public sealed class Reservation : BaseEntity, IBaseEntity
    {
        public Reservation(int reservationId,
                   DateTimeOffset dateFrom,
                   DateTimeOffset dateTo,
                   int capacity,
                   int roomId,
                   bool isDeleted)
        {
            ReservationId = reservationId;
            DateFrom = dateFrom;
            DateTo = dateTo;
            Capacity = capacity;
            RoomId = roomId;
            IsDeleted = isDeleted;
        }

        public int ReservationId { get; }
        public Date DateFrom { get; private set; }
        public Date DateTo { get; private set; }
        public Capacity Capacity { get; private set; }
        public bool? IsDeleted { get; set; }
        public int RoomId { get; private set; }

        public Room Room { get; set; }
    }
}
