using TriviaBoxServer.Models.Enum;

namespace TriviaBoxServer.Models.Entities
{
    public class Game
    {
        public int GameId { get; set; }
        public GameType Type { get; set; }        
        public GameMechanics GameMechanics { get; set; }
    }
}
