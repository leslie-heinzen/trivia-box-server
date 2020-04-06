using System.ComponentModel;

namespace TriviaBoxServer.Models.Enum
{
    public enum GameType
    {
        [Description("General")]
        General = 1,
        [Description("Sports")]
        Sports = 2,
        [Description("Science")]
        Science = 3,
        [Description("Music")]
        Music = 4
    }
}
