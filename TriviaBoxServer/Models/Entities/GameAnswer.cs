using System.Text.Json.Serialization;

namespace TriviaBoxServer.Models.Entities
{
    public class GameAnswer
    {
        public int GameAnswerId { get; set; }
        public string Answer { get; set; }
        public bool Correct { get; set; }
        public int GameQuestionId { get; set; }
        [JsonIgnore]
        public GameQuestion GameQuestion { get; set; }
    }
}