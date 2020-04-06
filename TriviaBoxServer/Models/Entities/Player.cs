using System.Text.Json.Serialization;

namespace TriviaBoxServer.Models.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int RoomId { get; set; }
        [JsonIgnore]
        public Room Room { get; set; }
    }
}
