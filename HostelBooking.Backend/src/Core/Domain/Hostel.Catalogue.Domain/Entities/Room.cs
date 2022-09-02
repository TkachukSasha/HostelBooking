using Hostel.Catalogue.Domain.ValueObjects;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Domain.Entities
{
    public sealed class Room : BaseEntity, IBaseEntity
    {
        public Room()
        {

        }

        public Room(int number,
                    int floor,
                    int capacity,
                    bool isDeleted,
                    int companyId)
        {
            Number = number;
            Floor = floor;
            Capacity = capacity;
            IsDeleted = isDeleted;
            CompanyId = companyId;
        }

        public int RoomId { get; private set; }
        public Number Number { get; private set; }
        public Floor Floor { get; private set; }
        public Capacity Capacity { get; private set; }
        public bool? IsDeleted { get; set; }
        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
