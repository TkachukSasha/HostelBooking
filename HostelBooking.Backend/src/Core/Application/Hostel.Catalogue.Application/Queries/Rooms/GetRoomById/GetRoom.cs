using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Queries.Rooms.GetRoomById
{
    public class GetRoom : IQuery<Room>
    {
        public int RoomId { get; set; }
    }

    public class GetRoomQueryHandler : IQueryHandler<GetRoom, Room>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public GetRoomQueryHandler(IRoomRepository roomRepository,
                                   IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<Room> HandleAsync(GetRoom query)
        {
            var existingRoom = await _roomRepository.GetRoomById(query.RoomId);

            return existingRoom;
        }
    }
}
