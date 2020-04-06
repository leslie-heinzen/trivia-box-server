using Microsoft.AspNetCore.SignalR;
using TriviaBoxServer.Managers;
using TriviaBoxServer.Models.Entities;
using TriviaBoxServer.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaBoxServer
{
    public class RoomHub : Hub
    {
        IRoomManager _roomManager;
        public RoomHub(IRoomManager roomManager)
        {
            _roomManager = roomManager;
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var player = await _roomManager.GetPlayerByConnectionId(Context.ConnectionId);
            var room = await _roomManager.GetRoomByConnectionId(Context.ConnectionId);

            if (player.Error != null && room.Error != null)
            {
                await base.OnDisconnectedAsync(exception);
            }
            else if (player.Error != null)
            {
                await RemoveFromRoom(Context.ConnectionId, room.Item.Room.RoomCode, "THE HOST", exception);
            }
            else if (room.Error != null)
            {
                await RemoveFromRoom(Context.ConnectionId, player.Item.Player.Room.RoomCode, player.Item.Player.Name, exception);
            }
        } 

        public async Task JoinRoomAsPlayer(string player, string roomCode)
        {
            if (!string.IsNullOrWhiteSpace(player))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
                await _roomManager.AddPlayerToRoom(new AddPlayerRequest
                {
                    Name = player,
                    ConnectionId = Context.ConnectionId,
                    RoomCode = roomCode
                });
                var room = await _roomManager.GetRoom(roomCode);
                await Clients.Group(roomCode)
                    .SendAsync("sendPlayersUpdate", $"{player} has joined the game.");
                await Clients.Group(roomCode)
                    .SendAsync("sendPlayers", room.Item.Room.Players);
            }            
        }

        public async Task JoinRoomAsHost(string roomCode)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
            await _roomManager.UpdateRoomConnectionId(Context.ConnectionId, roomCode);
        }

        public async Task StartGame(string roomCode)
        {
            var roomReq = await _roomManager.GetRoom(roomCode);
            var room = roomReq.Item.Room;

            // Run Game
            var game = room.Game;
            var mechanics = game.GameMechanics;

            for (var i = 0; i < mechanics.Questions.Count; i++)
            {
                // GameState is now Active
                room = await GetRoom(roomCode);

                if (room.State == Models.Enum.GameState.Ended)
                {
                    break;
                }

                room.State = Models.Enum.GameState.Active;
                await _roomManager.UpdateRoomByRoomCode(roomCode, room);
                await Clients.Group(roomCode)
                    .SendAsync("sendGameState", room.State);
                await Clients.Group(roomCode)
                    .SendAsync("sendPlayers", room.Players);

                var currentQuestion = mechanics.Questions[i];
                await Clients.Group(roomCode)
                    .SendAsync("sendQuestion", currentQuestion);
                await Task.Delay(30000);

                // between rounds (except last round)
                if (i < mechanics.Questions.Count - 1)
                {
                    // GameState is paused
                    room = await GetRoom(roomCode);
                    room.State = Models.Enum.GameState.BetweenRounds;
                    await _roomManager.UpdateRoomByRoomCode(roomCode, room);
                    await Clients.Group(roomCode)
                        .SendAsync("sendGameState", room.State);
                    await Clients.Group(roomCode)
                        .SendAsync("sendPlayers", room.Players);
                    await Task.Delay(10000);
                }
            }

            // Game has Ended, update GameState
            room = await GetRoom(roomCode);
            room.State = Models.Enum.GameState.Ended;
            await _roomManager.UpdateRoomByRoomCode(roomCode, room);
            await Clients.Group(roomCode)
                .SendAsync("sendGameState", room.State);
            await Clients.Group(roomCode).SendAsync("sendWinnerMessage", SelectWinner(room.Players));
        }

        public async Task SubmitAnswer(bool correct)
        {
            if (correct)
            {
                await _roomManager.IncrementPlayerScore(Context.ConnectionId);
            }
        }

        private string SelectWinner(List<Player> players)
        {
            var max = players.Max(p => p.Score);
            var winners = players.Where(p => p.Score == max);

            var sb = new StringBuilder();

            if (winners.Count() > 1)
            {
                sb.Append("welp. looks like we have ourselves a tie. ");
            }

            var names = winners.Select(w => w.Name);
            sb.Append("congrats, ");
            sb.Append(string.Join(" and ", names));
            sb.Append(".");

            return sb.ToString();
        }

        private async Task RemoveFromRoom(string contextId, string roomCode, string name, Exception exception)
        {
            if (name != "THE HOST")
            {
                await _roomManager.RemovePlayerFromRoom(contextId, roomCode);
                var updatedRoom = await GetRoom(roomCode);
                await Clients.Group(roomCode)
                    .SendAsync("sendPlayersUpdate", $"{name} has disconnected.", updatedRoom.Players);
                await Clients.Group(roomCode)
                    .SendAsync("sendPlayers", updatedRoom.Players);
            } else
            {
                var room = await GetRoom(roomCode);
                room.State = Models.Enum.GameState.Ended;
                await _roomManager.UpdateRoomByRoomCode(roomCode, room);
                await Clients.Group(roomCode)
                    .SendAsync("sendPlayersUpdate", $"{name} has disconnected.", room.Players);
                await Clients.Group(roomCode)
                    .SendAsync("sendGameState", room.State);
            }
            
            await Groups.RemoveFromGroupAsync(contextId, roomCode);
            await base.OnDisconnectedAsync(exception);
        }

        private async Task<Room> GetRoom(string roomCode)
        {
            var roomReq = await _roomManager.GetRoom(roomCode);
            return roomReq.Item.Room;
        }
    }
}