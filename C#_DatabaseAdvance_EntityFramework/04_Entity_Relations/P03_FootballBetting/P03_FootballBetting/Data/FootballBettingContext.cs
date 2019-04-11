using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;

    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {

        }

        public FootballBettingContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            TeamModelCreating(modelBuilder);

            TownModelCreating(modelBuilder);

            GameModelCreating(modelBuilder);

            PlayerModelCreating(modelBuilder);

            PlayerStatisticsModelCreating(modelBuilder);

            BetModelCreating(modelBuilder);

            UserModelCreating(modelBuilder);
        }

        private void UserModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.HasMany(u => u.Bets)
                    .WithOne(b => b.User);
            });
        }

        private void BetModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(b => b.BetId);

                entity.Property(b => b.Prediction).IsRequired(true);

                entity
                    .HasOne(b => b.Game)
                    .WithMany(g => g.Bets);
            });
        }

        private void PlayerStatisticsModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PlayerStatistic>(entity =>
                {
                    entity
                        .HasKey(ps => new {ps.GameId, ps.PlayerId});

                    entity
                        .HasOne(ps => ps.Player)
                        .WithMany(e => e.PlayerStatistics)
                        .HasForeignKey(ps => ps.PlayerId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity
                        .HasOne(ps => ps.Game)
                        .WithMany(g => g.PlayerStatistics)
                        .HasForeignKey(ps => ps.GameId)
                        .OnDelete(DeleteBehavior.Restrict);
                });
        }

        private void PlayerModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity
                    .HasKey(p => p.PlayerId);

                entity
                    .HasOne(p => p.Team)
                    .WithMany(t => t.Players);

                entity
                    .HasOne(p => p.Position)
                    .WithMany(p => p.Players);
            });
        }

        private void GameModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity
                    .HasKey(g => g.GameId);

                entity.Property(g => g.DateTime).IsRequired(true);

                entity
                    .HasOne(g => g.HomeTeam)
                    .WithMany(g => g.HomeGames)
                    .HasForeignKey(g => g.HomeTeamId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(g => g.AwayTeam)
                    .WithMany(g => g.AwayGames)
                    .HasForeignKey(g => g.AwayTeamId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void TownModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Town>(entity =>
                {
                    entity
                        .HasKey(t => t.TownId);

                    entity
                        .HasOne(t => t.Country)
                        .WithMany(t => t.Towns);
                });
        }

        private void TeamModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(entity =>
            {
                entity
                    .HasKey(t => t.TeamId);

                entity
                    .Property(t => t.Initials)
                    .HasColumnType("CHAR(3)");

                entity
                    .HasOne(pm => pm.PrimaryKitColor)
                    .WithMany(t => t.PrimaryKitTeams)
                    .HasForeignKey(e => e.PrimaryKitColorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(sk => sk.SecondaryKitColor)
                    .WithMany(t => t.SecondaryKitTeams)
                    .HasForeignKey(sk => sk.SecondaryKitColorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(t => t.Town)
                    .WithMany(t => t.Teams);
            });
        }
    }
}