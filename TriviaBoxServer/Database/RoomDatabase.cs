using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TriviaBoxServer.Models.Entities;
using TriviaBoxServer.Models.Enum;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaBoxServer.Database
{
    public class RoomDatabase : IRoomDatabase
    {
        private readonly IServiceProvider _s;
        public RoomDatabase(IServiceProvider s)
        {
            _s = s;
        }

        public async Task<string> AddRoom(GameType gameId)
        {
            using (var scope = _s.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<RoomDbContext>();
                var roomCode = GenerateCode();
                await _context.Rooms.AddAsync(new Room
                {
                    GameId = (int)gameId,
                    RoomCode = roomCode
                });

                await _context.SaveChangesAsync();

                return roomCode;
            }

        }

        public async Task<Room> GetRoom(string roomCode)
        {
            using (var scope = _s.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<RoomDbContext>();
                var room = await _context.Rooms
                    .Include(r => r.Players)
                    .Include(r => r.Game)
                        .ThenInclude(g => g.GameMechanics)
                            .ThenInclude(g => g.Questions)
                                .ThenInclude(gq => gq.GameAnswers)
                    .FirstOrDefaultAsync(r => r.RoomCode == roomCode);

                await _context.Entry(room).ReloadAsync();
                return room;
            }

        }

        public async Task<Player> AddPlayerToRoom(string name, string connectionId, string roomCode)
        {
            using (var scope = _s.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<RoomDbContext>();
                var room = await _context.Rooms
                    .FirstOrDefaultAsync(r => r.RoomCode == roomCode);

                await _context.Players.AddAsync(new Player
                {
                    Name = name,
                    ConnectionId = connectionId,
                    RoomId = room.RoomId,
                    Score = 0
                });

                await _context.SaveChangesAsync();

                return await _context.Players
                    .Include(p => p.Room)
                    .FirstOrDefaultAsync(p => p.RoomId == room.RoomId);
            }

        }

        public async Task IncrementPlayerScore(string connectionId, int increment = 1)
        {
            using (var scope = _s.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<RoomDbContext>();
                var player = await _context.Players
                    .FirstOrDefaultAsync(p => p.ConnectionId == connectionId);
                player.Score += increment;

                _context.Players.Update(player);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<Player> GetPlayerByConnectionId(string connectionId)
        {
            using (var scope = _s.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<RoomDbContext>();
                return await _context.Players
                    .Include(p => p.Room)
                    .FirstOrDefaultAsync(p => p.ConnectionId == connectionId);
            }

        }

        public async Task<Room> GetRoomByConnectionId(string connectionId)
        {
            using (var scope = _s.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<RoomDbContext>();
                return await _context.Rooms.FirstOrDefaultAsync(r => r.ConnectionId == connectionId);
            }
        }

        public async Task UpdateRoomByRoomCode(string roomCode, Room room)
        {
            using (var scope = _s.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<RoomDbContext>();
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateRoomConnectionId(string connectionId, string roomCode)
        {
            using (var scope = _s.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<RoomDbContext>();
                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomCode == roomCode);
                room.ConnectionId = connectionId;
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemovePlayerFromRoom(string connectionId, string roomCode)
        {
            using (var scope = _s.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<RoomDbContext>();
                var player = await _context.Players.FirstOrDefaultAsync(p => p.ConnectionId == connectionId);
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
        }

        private string GenerateCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 4)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
