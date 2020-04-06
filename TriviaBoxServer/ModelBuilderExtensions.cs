using Microsoft.EntityFrameworkCore;
using TriviaBoxServer.Models.Entities;

namespace TriviaBoxServer
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasData(new Game
                {
                    GameId = 1,
                    Type = Models.Enum.GameType.General
                });

            modelBuilder.Entity<GameMechanics>()
                .HasData(new GameMechanics
                {
                    GameMechanicsId = 1,
                    Rounds = 10,
                    RoundTime = 20000, // ms
                    GameId = 1
                });

            modelBuilder.Entity<GameQuestion>()
                .HasData(new GameQuestion
                {
                    GameQuestionId = 1,
                    GameMechanicsId = 1,
                    Question = "When Michael Jordan played for the Chicago Bulls, how many NBA Championships did he win?"
                },
                new GameQuestion
                {
                    GameQuestionId = 2,
                    GameMechanicsId = 1,
                    Question = "Which planet is the hottest in the solar system?"
                },
                new GameQuestion
                {
                    GameQuestionId = 3,
                    GameMechanicsId = 1,
                    Question = "What is the symbol for potassium?"
                },
                new GameQuestion
                {
                    GameQuestionId = 4,
                    GameMechanicsId = 1,
                    Question = "In what year was the Corvette introduced?"
                },
                new GameQuestion
                {
                    GameQuestionId = 5,
                    GameMechanicsId = 1,
                    Question = "What does BMW stand for (in English)?"
                });

            modelBuilder.Entity<GameAnswer>()
                .HasData(new GameAnswer
                {
                    GameAnswerId = 1,
                    GameQuestionId = 1,
                    Answer = "Six",
                    Correct = true
                },
                new GameAnswer
                {
                    GameAnswerId = 2,
                    GameQuestionId = 1,
                    Answer = "Three"
                },
                new GameAnswer
                {
                    GameAnswerId = 3,
                    GameQuestionId = 1,
                    Answer = "One"
                },
                new GameAnswer
                {
                    GameAnswerId = 4,
                    GameQuestionId = 1,
                    Answer = "LEBRON"
                },
                new GameAnswer
                {
                    GameAnswerId = 5,
                    GameQuestionId = 2,
                    Answer = "Mercury"
                },
                new GameAnswer
                {
                    GameAnswerId = 6,
                    GameQuestionId = 2,
                    Answer = "Saturn"
                },
                new GameAnswer
                {
                    GameAnswerId = 7,
                    GameQuestionId = 2,
                    Answer = "Venus",
                    Correct = true
                },
                new GameAnswer
                {
                    GameAnswerId = 8,
                    GameQuestionId = 2,
                    Answer = "URF"
                },
                new GameAnswer
                {
                    GameAnswerId = 9,
                    GameQuestionId = 3,
                    Answer = "K",
                    Correct = true
                },
                new GameAnswer
                {
                    GameAnswerId = 10,
                    GameQuestionId = 3,
                    Answer = "He"
                },
                new GameAnswer
                {
                    GameAnswerId = 11,
                    GameQuestionId = 3,
                    Answer = "C"
                },
                new GameAnswer
                {
                    GameAnswerId = 12,
                    GameQuestionId = 3,
                    Answer = "Bo"
                },
                new GameAnswer
                {
                    GameAnswerId = 13,
                    GameQuestionId = 4,
                    Answer = "1951"
                },
                new GameAnswer
                {
                    GameAnswerId = 14,
                    GameQuestionId = 4,
                    Answer = "1961"
                },
                new GameAnswer
                {
                    GameAnswerId = 15,
                    GameQuestionId = 4,
                    Answer = "1953",
                    Correct = true
                },
                new GameAnswer
                {
                    GameAnswerId = 16,
                    GameQuestionId = 4,
                    Answer = "1948"
                },
                new GameAnswer
                {
                    GameAnswerId = 17,
                    GameQuestionId = 5,
                    Answer = "Bavarian Motor Works",
                    Correct = true
                },
                new GameAnswer
                {
                    GameAnswerId = 18,
                    GameQuestionId = 5,
                    Answer = "Bone Mender Wanda"
                },
                new GameAnswer
                {
                    GameAnswerId = 19,
                    GameQuestionId = 5,
                    Answer = "Brittle Mice Wincing"
                },
                new GameAnswer
                {
                    GameAnswerId = 20,
                    GameQuestionId = 5,
                    Answer = "Bananas Migrating West"
                });
        }
    }
}
