using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Application.Dto.Room;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Commands.Rooms.Delete
{
    public class DeleteRoom : ICommand<RoomReturnDto>
    {
    }

    public class DeleteRoomCommandHandler : ICommandHandler<DeleteRoom, RoomReturnDto>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRoomCommandHandler(IRoomRepository roomRepository,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RoomReturnDto> HandleAsync(DeleteRoom command, CancellationToken cancellationToken)
        {
            var room = _mapper.Map<Room>(command);

            await _roomRepository.DeleteRoom(room.RoomId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var returnRoom = _mapper.Map<RoomReturnDto>(room);

            return returnRoom;
        }
    }
}
