using Microsoft.EntityFrameworkCore;
using TriviaBoxServer.Models.Entities;

namespace TriviaBoxServer
{
    public class RoomDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameMechanics> GameMechanics { get; set; }
        public DbSet<GameQuestion> GameQuestions { get; set; }
        public DbSet<GameAnswer> GameAnswers { get; set; }

        public RoomDbContext(DbContextOptions<RoomDbContext> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .Property(p => p.Type)
                .HasConversion<int>();

            modelBuilder.Entity<Room>()
                .Property(p => p.State)
                .HasConversion<int>();

            modelBuilder.Seed();
        }
    }
}