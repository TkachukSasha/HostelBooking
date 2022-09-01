using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Commands.Rooms.Update
{
    public class UpdateRoom : ICommand
    {
        public int RoomId { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public bool? IsDeleted { get; set; }
        public int CompanyId { get; set; }
    }

    public class UpdateRoomCommandHandler : ICommandHandler<UpdateRoom, int>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoomCommandHandler(IRoomRepository roomRepository,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> HandleAsync(UpdateRoom command, CancellationToken cancellationToken)
        {
            var room = _mapper.Map<Room>(command);

            await _roomRepository.UpdateRoom(room);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return room.CompanyId;
        }
    }
}
