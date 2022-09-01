using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Commands.Rooms.Create
{
    public class CreateRoom : ICommand
    {
        public int RoomId { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public bool? IsDeleted { get; set; }
        public int CompanyId { get; set; }
    }

    public class CreateRoomCommandHandler : ICommandHandler<CreateRoom, int>
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

        public async Task<int> HandleAsync(CreateRoom command, CancellationToken cancellationToken)
        {
            var room = _mapper.Map<Room>(command);

            await _roomRepository.AddRoom(room);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return room.CompanyId;
        }
    }
}
