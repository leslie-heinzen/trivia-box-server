using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TriviaBoxServer.Models.Entities
{
    public class GameMechanics
    {
        public int GameMechanicsId { get; set; }
        public int Rounds { get; set; }
        public int RoundTime { get; set; }
        public List<GameQuestion> Questions { get; set; }
        public int GameId { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }
    }
}
