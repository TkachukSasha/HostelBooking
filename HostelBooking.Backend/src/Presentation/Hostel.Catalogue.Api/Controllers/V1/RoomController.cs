using AutoMapper;
using Hostel.Catalogue.Application.Commands.Rooms.Create;
using Hostel.Catalogue.Application.Commands.Rooms.Delete;
using Hostel.Catalogue.Application.Commands.Rooms.Update;
using Hostel.Catalogue.Application.Dto.Room;
using Hostel.Catalogue.Application.Queries.Rooms.GetListRooms;
using Hostel.Catalogue.Application.Queries.Rooms.GetRoomById;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;
using Hostel.Shared.Types.Const;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hostel.Catalogue.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ICommandHandler<CreateRoom, RoomReturnDto> _createRoomHandler;
        private readonly ICommandHandler<UpdateRoom, RoomReturnDto> _updateRoomHandler;
        private readonly ICommandHandler<DeleteRoom, RoomReturnDto> _deleteRoomHandler;
        private readonly IQueryHandler<GetRooms, IEnumerable<Room>> _getRoomsHandler;
        private readonly IQueryHandler<GetRoom, Room> _getRoomHandler;
        private readonly IMapper _mapper;

        public RoomController(ICommandHandler<CreateRoom, RoomReturnDto> createRoomHandle,
                              ICommandHandler<UpdateRoom, RoomReturnDto> updateRoomHandler,
                              ICommandHandler<DeleteRoom, RoomReturnDto> deleteRoomHandler,
                              IQueryHandler<GetRooms, IEnumerable<Room>> getRoomsHandler,
                              IQueryHandler<GetRoom, Room> getRoomHandler,
                              IMapper mapper)
        {
            _createRoomHandler = createRoomHandle;
            _updateRoomHandler = updateRoomHandler;
            _deleteRoomHandler = deleteRoomHandler;
            _getRoomsHandler = getRoomsHandler;
            _getRoomHandler = getRoomHandler;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all available rooms
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route(Routes.GetRooms)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms([FromQuery] GetRooms query)
        {
            var rooms = await _getRoomsHandler.HandleAsync(query);
            return Ok(rooms);
        }

        /// <summary>
        /// Get room by id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route(Routes.GetRoomById)]
        [HttpGet]
        public async Task<ActionResult<Room>> GetRoomById([FromQuery] GetRoom query)
        {
            var room = await _getRoomHandler.HandleAsync(new GetRoom { RoomId = query.RoomId});
            return Ok(room);
        }

        /// <summary>
        /// Create room
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Route(Routes.CreateRoom)]
        [HttpPost]
        public async Task<ActionResult<RoomReturnDto>> CreateRoom([FromBody] CreateRoom request, CancellationToken cancellationToken)
        {
            var room = await _createRoomHandler.HandleAsync(request, cancellationToken);
            return Ok(room);
        }

        /// <summary>
        /// Update room
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Route(Routes.UpdateRoom)]
        [HttpPut]
        public async Task<ActionResult<RoomReturnDto>> UpdateRoom([FromBody] UpdateRoom request, CancellationToken cancellationToken)
        {
            var room = await _updateRoomHandler.HandleAsync(request, cancellationToken);
            return Ok(room);
        }

        /// <summary>
        /// Delete room
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Route(Routes.DeleteRoom)]
        [HttpDelete]
        public async Task<ActionResult<RoomReturnDto>> DeleteRoom([FromBody] DeleteRoom request, CancellationToken cancellationToken)
        {
            var room = await _deleteRoomHandler.HandleAsync(request, cancellationToken);
            return Ok(room);
        }
    }
}
