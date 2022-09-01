using Hostel.Catalogue.Application.Commands.Rooms.Create;
using Hostel.Catalogue.Application.Commands.Rooms.Delete;
using Hostel.Catalogue.Application.Commands.Rooms.Update;
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
        private readonly ICommandHandler<CreateRoom> _createRoomHandler;
        private readonly ICommandHandler<UpdateRoom> _updateRoomHandler;
        private readonly ICommandHandler<DeleteRoom> _deleteRoomHandler;
        private readonly IQueryHandler<GetRooms, IEnumerable<Room>> _getRoomsHandler;
        private readonly IQueryHandler<GetRoom, Room> _getRoomHandler;

        public RoomController(ICommandHandler<CreateRoom> createRoomHandle,
                              ICommandHandler<UpdateRoom> updateRoomHandler,
                              ICommandHandler<DeleteRoom> deleteRoomHandler,
                              IQueryHandler<GetRooms, IEnumerable<Room>> getRoomsHandler,
                              IQueryHandler<GetRoom, Room> getRoomHandler)
        {
            _createRoomHandler = createRoomHandle;
            _updateRoomHandler = updateRoomHandler;
            _deleteRoomHandler = deleteRoomHandler;
            _getRoomsHandler = getRoomsHandler;
            _getRoomHandler = getRoomHandler;
        }

        /// <summary>
        /// Get all available rooms
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet(Routes.GetRooms)]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms([FromQuery] GetRooms query)
        {
            var rooms = _getRoomsHandler.HandleAsync(query);
            return Ok(rooms);
        }

        /// <summary>
        /// Get room by id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet(Routes.GetRoomById)]
        public async Task<ActionResult<Room>> GetRoomById([FromQuery] GetRoom query)
        {
            var room = _getRoomHandler.HandleAsync(new GetRoom { RoomId = query.RoomId});
            return Ok(room);
        }

        /// <summary>
        /// Create room
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost(Routes.CreateRoom)]
        public async Task<ActionResult> CreateRoom([FromBody] CreateRoom request, CancellationToken cancellationToken)
        {
            var room = _createRoomHandler.HandleAsync(request, cancellationToken);
            return Ok(room);
        }

        /// <summary>
        /// Update room
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut(Routes.UpdateRoom)]
        public async Task<ActionResult> UpdateRoom([FromBody] UpdateRoom request, CancellationToken cancellationToken)
        {
            var room = _updateRoomHandler.HandleAsync(request, cancellationToken);
            return Ok(room);
        }

        /// <summary>
        /// Delete room
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete(Routes.DeleteRoom)]
        public async Task<ActionResult> DeleteRoom([FromBody] DeleteRoom request, CancellationToken cancellationToken)
        {
            var room = _deleteRoomHandler.HandleAsync(request, cancellationToken);
            return Ok(room);
        }
    }
}
