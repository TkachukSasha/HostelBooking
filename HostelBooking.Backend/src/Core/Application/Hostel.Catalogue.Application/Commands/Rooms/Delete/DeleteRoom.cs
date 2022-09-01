using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Commands.Rooms.Delete
{
    public class DeleteRoom : ICommand
    {
        public int RoomId { get; set; }
    }

    public class DeleteRoomCommandHandler : ICommandHandler<DeleteRoom, int>
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

        public async Task<int> HandleAsync(DeleteRoom command, CancellationToken cancellationToken)
        {
            var room = _mapper.Map<Room>(command);

            await _roomRepository.DeleteRoom(room.RoomId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return room.RoomId;
        }
    }
}
