using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Application.Dto.Room;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Commands.Rooms.Update
{
    public class UpdateRoom : ICommand<RoomReturnDto>
    {
        public int Number { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public bool? IsDeleted { get; set; }
        public int CompanyId { get; set; }
    }

    public class UpdateRoomCommandHandler : ICommandHandler<UpdateRoom, RoomReturnDto>
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

        public async Task<RoomReturnDto> HandleAsync(UpdateRoom command, CancellationToken cancellationToken)
        {
            var room = _mapper.Map<Room>(command);

            await _roomRepository.UpdateRoom(room);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var returnRoom = _mapper.Map<RoomReturnDto>(room);

            return returnRoom;
        }
    }
}
