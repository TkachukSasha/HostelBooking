using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Queries.Rooms.GetListRooms
{
    public class GetRooms : IQuery<IEnumerable<Room>>
    {
    }

    public class GetRoomsQueryHandler : IQueryHandler<GetRooms, IEnumerable<Room>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public GetRoomsQueryHandler(IRoomRepository roomRepository,
                                        IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Room>> HandleAsync(GetRooms query)
        {
            return await _roomRepository.GetAllRooms();
        }
    }
}
