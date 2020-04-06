using TriviaBoxServer.Models.Entities;
using TriviaBoxServer.Models.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TriviaBoxServer.Database
{
    public interface IRoomDatabase
    {
        public Task<string> AddRoom(GameType gameId);
        public Task<Player> AddPlayerToRoom(string name, string connectionId, string roomCode);
        public Task<Room> GetRoom(string roomCode);
        public Task IncrementPlayerScore(string connectionId, int increment = 1);
        public Task<Player> GetPlayerByConnectionId(string connectionId);
        public Task<Room> GetRoomByConnectionId(string connectionId);
        public Task UpdateRoomConnectionId(string connectionId, string roomCode);
        public Task UpdateRoomByRoomCode(string roomCode, Room room);
        Task RemovePlayerFromRoom(string connectionId, string roomCode);
    }
}
