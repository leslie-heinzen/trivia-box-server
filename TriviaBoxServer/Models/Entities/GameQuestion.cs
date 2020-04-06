using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TriviaBoxServer.Models.Entities
{
    public class GameQuestion
    {
        public int GameQuestionId { get; set; }
        public string Question { get; set; }
        public List<GameAnswer> GameAnswers { get; set; }
        public int GameMechanicsId { get; set; }
        [JsonIgnore]
        public GameMechanics GameMechanics { get; set; }
    }
}