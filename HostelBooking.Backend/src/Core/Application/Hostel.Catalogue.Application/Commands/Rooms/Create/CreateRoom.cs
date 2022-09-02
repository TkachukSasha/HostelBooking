using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Application.Dto.Room;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Commands.Rooms.Create
{
    public class CreateRoom : ICommand<RoomReturnDto>
    {
        public int Number { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public bool? IsDeleted { get; set; }
        public int CompanyId { get; set; }
    }

    public class CreateRoomCommandHandler : ICommandHandler<CreateRoom, RoomReturnDto>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoomCommandHandler(IRoomRepository roomRepository,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RoomReturnDto> HandleAsync(CreateRoom command, CancellationToken cancellationToken)
        {
            var room = _mapper.Map<Room>(command);

            await _roomRepository.AddRoom(room);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var returnRoom = _mapper.Map<RoomReturnDto>(room);

            return returnRoom;
        }
    }
}
