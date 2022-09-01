using Hostel.Catalogue.Domain.Entities;

namespace Hostel.Catalogue.Domain.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> GetRoomById(int id);
        Task<IEnumerable<Room>> GetAllRooms();
        Task<int> AddRoom(Room room);
        Task<int> UpdateRoom(Room room);
        Task<int> DeleteRoom(int roomId);
    }
}
