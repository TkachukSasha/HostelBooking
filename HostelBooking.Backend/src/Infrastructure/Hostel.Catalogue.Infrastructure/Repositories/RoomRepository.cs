using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Catalogue.Infrastructure.Dal;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Hostel.Catalogue.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository, IDisposable
    {
        private CatalogueContext _context;
        private readonly ILogger _logger;
        private bool _disposed;

        public RoomRepository(CatalogueContext context,
                              ILogger logger)
        {
            (_context, _logger) = (context, logger);
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            try
            {
                return await _context.Room.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke get all rooms method");
                throw;
            }
        }

        public async Task<Room> GetRoomById(int id)
        {
            try
            {
                return await _context.Room.FirstOrDefaultAsync(x => x.RoomId == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke get room by id method");
                throw;
            }
        }

        public async Task<int> AddRoom(Room room)
        {
            try
            {
                await _context.Room.AddAsync(room);
                await _context.SaveChangesAsync();
                return room.RoomId;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke add room method");
                throw;
            }
        }

        public async Task<int> UpdateRoom(Room room)
        {
            try
            {
                _context.Room.Update(room);
                await _context.SaveChangesAsync();
                return room.RoomId;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke update room method");
                throw;
            }
        }

        public async Task<int> DeleteRoom(int roomId)
        {
            try
            {
                var existRoom = await _context.Room.FirstOrDefaultAsync(x => x.RoomId == roomId);

                _context.Room.Remove(existRoom);
                await _context.SaveChangesAsync();
                return existRoom.RoomId;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke delete room method");
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            _ = disposing;

            if (!_disposed)
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

            _disposed = true;
        }
    }
}
