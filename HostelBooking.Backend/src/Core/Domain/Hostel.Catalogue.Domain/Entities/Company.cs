using Hostel.Catalogue.Domain.ValueObjects;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Domain.Entities
{
    public sealed class Company : BaseEntity, IBaseEntity
    {
        public Company()
        {
            Rooms = new HashSet<Room>();
        }

        public Company(string name,
                       string description,
                       string city)
        {
            Name = name;
            Description = description;
            City = city;
        }

        public int CompanyId { get; set; }
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public City City { get; private set; }
        public bool? IsDeleted { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
