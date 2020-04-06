using TriviaBoxServer.Models.Enum;
using System.Collections.Generic;

namespace TriviaBoxServer.Models.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public string ConnectionId { get; set; }
        public string RoomCode { get; set; }
        public GameState State { get; set; }
        public List<Player> Players { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
