using Hostel.Catalogue.Domain.ValueObjects;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Domain.Entities
{
    public sealed class Room : BaseEntity, IBaseEntity
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
        }

        public Room(int number,
                    int floor,
                    int capacity)
        {
            Number = number;
            Floor = floor;
            Capacity = capacity;
        }

        public int RoomId { get; set; }
        public Number Number { get; private set; }
        public Floor Floor { get; private set; }
        public Capacity Capacity { get; private set; }
        public bool? IsDeleted { get; set; }
        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
