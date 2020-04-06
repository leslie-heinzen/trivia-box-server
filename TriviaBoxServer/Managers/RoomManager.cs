using TriviaBoxServer.Database;
using TriviaBoxServer.Models.Entities;
using TriviaBoxServer.Models.Request;
using TriviaBoxServer.Models.Response;
using System.Threading.Tasks;

namespace TriviaBoxServer.Managers
{
    public class RoomManager : IRoomManager
    {
        private readonly IRoomDatabase _database;
        public RoomManager(IRoomDatabase database)
        {
            _database = database;
        }

        public async Task<Result<AddRoomResponse>> AddRoom(AddRoomRequest request)
        {
            var roomCode = await _database.AddRoom(request.GameId);
            if (roomCode != null)
            {
                return Result.FromItem(new AddRoomResponse { RoomCode = roomCode });
            }

            return Result.FromError<AddRoomResponse>(System.Net.HttpStatusCode.BadRequest);
        }

        public async Task<Result<GetRoomResponse>> GetRoom(string roomCode)
        {
            var room = await _database.GetRoom(roomCode);
            if (room != null)
            {
                return Result.FromItem(new GetRoomResponse { Room = room });
            }

            return Result.FromError<GetRoomResponse>(System.Net.HttpStatusCode.BadRequest);
        }

        public async Task IncrementPlayerScore(string connectionId)
        {
            await _database.IncrementPlayerScore(connectionId);
        }

        public async Task<Result<AddPlayerResponse>> AddPlayerToRoom(AddPlayerRequest request)
        {
            var player = await _database.AddPlayerToRoom(request.Name, request.ConnectionId, request.RoomCode);
            if (player != null)
            {
                return Result.FromItem(new AddPlayerResponse { Player = player });
            }

            return Result.FromError<AddPlayerResponse>(System.Net.HttpStatusCode.BadRequest);
        }

        public async Task<Result<GetPlayerByConnectionIdResponse>> GetPlayerByConnectionId(string connectionId)
        {
            var player = await _database.GetPlayerByConnectionId(connectionId);
            if (player != null)
            {
                return Result.FromItem(new GetPlayerByConnectionIdResponse { Player = player });
            }

            return Result.FromError<GetPlayerByConnectionIdResponse>(System.Net.HttpStatusCode.BadRequest);
        }

        public async Task<Result<GetRoomByConnectionIdResponse>> GetRoomByConnectionId(string connectionId)
        {
            var room = await _database.GetRoomByConnectionId(connectionId);
            if (room != null)
            {
                return Result.FromItem(new GetRoomByConnectionIdResponse { Room = room });
            }

            return Result.FromError<GetRoomByConnectionIdResponse>(System.Net.HttpStatusCode.BadRequest);
        }

        public async Task UpdateRoomByRoomCode(string roomCode, Room room)
        {
            await _database.UpdateRoomByRoomCode(roomCode, room);
        }

        public async Task UpdateRoomConnectionId(string connectionId, string roomCode)
        {
            await _database.UpdateRoomConnectionId(connectionId, roomCode);
        }

        public async Task RemovePlayerFromRoom(string connectionId, string roomCode)
        {
            await _database.RemovePlayerFromRoom(connectionId, roomCode);
        }
    }
}
