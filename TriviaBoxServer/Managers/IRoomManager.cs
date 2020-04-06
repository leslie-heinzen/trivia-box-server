using TriviaBoxServer.Models.Entities;
using TriviaBoxServer.Models.Request;
using TriviaBoxServer.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TriviaBoxServer.Managers
{
    public interface IRoomManager
    {
        public Task<Result<AddRoomResponse>> AddRoom(AddRoomRequest request);
        public Task<Result<GetRoomResponse>> GetRoom(string roomCode);
        public Task IncrementPlayerScore(string connectionId);
        public Task<Result<AddPlayerResponse>> AddPlayerToRoom(AddPlayerRequest request);
        public Task<Result<GetPlayerByConnectionIdResponse>> GetPlayerByConnectionId(string connectionId);
        public Task<Result<GetRoomByConnectionIdResponse>> GetRoomByConnectionId(string connectionId);
        public Task UpdateRoomConnectionId(string connectionId, string roomCode);
        public Task UpdateRoomByRoomCode(string roomCode, Room room);
        Task RemovePlayerFromRoom(string connectionId, string roomCode);
    }
}
