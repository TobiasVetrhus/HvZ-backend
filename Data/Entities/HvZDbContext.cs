using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Data.Entities
{
    public class HvZDbContext : DbContext
    {
        public HvZDbContext(DbContextOptions<HvZDbContext> options) : base(options)
        {
        }

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Kill> Kills { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Squad> Squads { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<PlayerKillRole> PlayerKillRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship between Conversation and Message
            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Conversation)
                .HasForeignKey(m => m.ConversationId);

            // Configure one-to-many relationship between User and Player
            modelBuilder.Entity<User>()
                .HasMany(u => u.Players)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            // Configure one-to-many relationship between Game and Mission
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Missions)
                .WithOne(m => m.Game)
                .HasForeignKey(m => m.GameId)
                .IsRequired(false);

            // Configure one-to-many relationship between Game and Player
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Players)
                .WithOne(p => p.Game)
                .HasForeignKey(p => p.GameId)
                .IsRequired(false);

            // Configure one-to-many relationship between Game and Conversation
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Conversations)
                .WithOne(p => p.Game)
                .HasForeignKey(p => p.GameId)
                .IsRequired(false);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.PlayerRolesInKills)
                .WithOne(k => k.Player)
                .HasForeignKey(k => k.PlayerId);

            modelBuilder.Entity<Kill>()
                .HasMany(p => p.PlayerRoles)
                .WithOne(k => k.Kill)
                .HasForeignKey(k => k.KillId);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Messages)
                .WithOne(m => m.Player)
                .HasForeignKey(m => m.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            // Seed data for Conversations
            modelBuilder.Entity<Conversation>().HasData(
                new Conversation { Id = 1, ConversationName = "Global Chat", ChatType = ChatType.Global, GameId = 1 },
                new Conversation { Id = 2, ConversationName = "Alpha Squad Chat", ChatType = ChatType.Squad, GameId = 1 },
                new Conversation { Id = 3, ConversationName = "Zombie Faction Chat", ChatType = ChatType.FactionZombies, GameId = 1 },
                new Conversation { Id = 4, ConversationName = "Human Faction Chat", ChatType = ChatType.FactionHumans, GameId = 1 }
            );

            // Seed data for Games
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Title = "HvZ Spring 2023",
                    Description = "Humans vs. Zombies - Spring 2023 Edition",
                    GameState = GameStatus.Registration,
                    PictureURL = "https://images.pexels.com/photos/6473550/pexels-photo-6473550.jpeg",
                },
                new Game
                {
                    Id = 2,
                    Title = "HvZ Summer 2023",
                    Description = "Humans vs. Zombies - Summer 2023 Edition",
                    GameState = GameStatus.InProgress,
                    PictureURL = "https://images.pexels.com/photos/6473550/pexels-photo-6473550.jpeg"
                },
                new Game
                {
                    Id = 3,
                    Title = "HvZ Fall 2023",
                    Description = "Humans vs. Zombies - Fall 2023 Edition",
                    GameState = GameStatus.InProgress,
                    PictureURL = "https://images.pexels.com/photos/6473550/pexels-photo-6473550.jpeg"
                },
                new Game
                {
                    Id = 4,
                    Title = "HvZ Winter 2023",
                    Description = "Humans vs. Zombies - Winter 2023 Edition",
                    GameState = GameStatus.Complete,
                    PictureURL = "https://images.pexels.com/photos/6473550/pexels-photo-6473550.jpeg"
                }
            );

            // Seed data for Kills
            modelBuilder.Entity<Kill>().HasData(
                new Kill { Id = 1, Description = "Player 2 tagged Player 1 as a zombie.", TimeOfKill = DateTime.Now },
                new Kill { Id = 2, Description = "Player 3 eliminated Zombie 1.", TimeOfKill = DateTime.Now },
                new Kill { Id = 3, Description = "Player 4 tagged Player 3.", TimeOfKill = DateTime.Now },
                new Kill { Id = 4, Description = "Player 1 tagged Player 4.", TimeOfKill = DateTime.Now }
            );

            // Seed data for Locations
            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, XCoordinate = 123, YCoordinate = 456 },
                new Location { Id = 2, XCoordinate = 789, YCoordinate = 101 },
                new Location { Id = 3, XCoordinate = 345, YCoordinate = 678 },
                new Location { Id = 4, XCoordinate = 987, YCoordinate = 543 }
            );

            // Seed data for Messages
            modelBuilder.Entity<Message>().HasData(
                new Message { Id = 1, Content = "Welcome to the game!", Sent = DateTime.Now, ConversationId = 1, PlayerId = 1 },
                new Message { Id = 2, Content = "Let's plan our strategy.", Sent = DateTime.Now, ConversationId = 2, PlayerId = 2 },
                new Message { Id = 3, Content = "Brains...", Sent = DateTime.Now, ConversationId = 3, PlayerId = 3 },
                new Message { Id = 4, Content = "Stay vigilant, humans.", Sent = DateTime.Now, ConversationId = 4, PlayerId = 4 }
            );

            // Seed data for Missions
            modelBuilder.Entity<Mission>().HasData(
                new Mission { Id = 1, Name = "Supply Run", Description = "Gather supplies from the designated location.", LocationId = 1, GameId = 1 },
                new Mission { Id = 2, Name = "Zombie Hunt", Description = "Hunt down and eliminate zombie players.", LocationId = 2, GameId = 2 },
                new Mission { Id = 3, Name = "Survival Challenge", Description = "Test your survival skills in the wilderness.", LocationId = 3, GameId = 3 },
                new Mission { Id = 4, Name = "Final Stand", Description = "Defend the safe zone from zombie attacks.", LocationId = 4, GameId = 4 }
            );

            // Seed data for Players
            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 1, Username = "Player1", Zombie = false, BiteCode = "BITE001", UserId = 1, LocationId = 1, SquadId = 1, GameId = 1 },
                new Player { Id = 2, Username = "Player2", Zombie = true, BiteCode = "ZOMBIE01", UserId = 2, LocationId = 2, SquadId = 2, GameId = 2 },
                new Player { Id = 3, Username = "Player3", Zombie = false, BiteCode = "BITE002", UserId = 3, LocationId = 3, SquadId = 3, GameId = 3 },
                new Player { Id = 4, Username = "Player4", Zombie = false, BiteCode = "BITE003", UserId = 4, LocationId = 4, SquadId = 4, GameId = 4 }
            );

            // Seed data for Rules
            modelBuilder.Entity<Rule>().HasData(
                new Rule { Id = 1, Title = "Safe Zones", Description = "Safe zones are designated areas where players cannot be tagged or eliminated. Seek refuge in these zones when needed." },
                new Rule { Id = 2, Title = "Zombie Transformation", Description = "When a human player is tagged by a zombie, they must put on a designated armband and join the zombie horde. Zombies can only tag players by hand, no props or weapons allowed." },
                new Rule { Id = 3, Title = "Mission Objectives", Description = "Missions are critical to the survival of humans. Complete mission objectives to earn rewards and secure vital supplies." },
                new Rule { Id = 4, Title = "Human Disguises", Description = "Humans can use disguises and props to blend in with the zombie horde for a limited time. Use this strategy wisely to avoid detection." }
            );

            // Seed data for Squads
            modelBuilder.Entity<Squad>().HasData(
                new Squad { Id = 1, SquadName = "Alpha Squad", NumberOfMembers = 5, NumberOfDeceased = 0 },
                new Squad { Id = 2, SquadName = "Zombie Horde", NumberOfMembers = 10, NumberOfDeceased = 3 },
                new Squad { Id = 3, SquadName = "Survivor Team", NumberOfMembers = 7, NumberOfDeceased = 1 },
                new Squad { Id = 4, SquadName = "Bravo Squad", NumberOfMembers = 6, NumberOfDeceased = 2 }
            );

            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com", Phone = "1234567890" },
                new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "janesmith@example.com", Phone = "9876543210" },
                new User { Id = 3, FirstName = "Zombie", LastName = "Walker", Email = "zombiewalker@example.com", Phone = "5555555555" },
                new User { Id = 4, FirstName = "Player", LastName = "Four", Email = "playerfour@example.com", Phone = "1111111111" }
            );


            // Seed data for PlayerKillRoles
            modelBuilder.Entity<PlayerKillRole>().HasData(
                new PlayerKillRole { Id = 1, RoleType = KillRoleType.Killer, PlayerId = 1, KillId = 1 },
                new PlayerKillRole { Id = 2, RoleType = KillRoleType.Victim, PlayerId = 2, KillId = 1 },
                new PlayerKillRole { Id = 3, RoleType = KillRoleType.Killer, PlayerId = 3, KillId = 2 },
                new PlayerKillRole { Id = 4, RoleType = KillRoleType.Victim, PlayerId = 1, KillId = 3 }
            );

            // Configure one-to-many relationship between Squad and Player
            modelBuilder.Entity<Squad>()
                    .HasMany(s => s.Players)
                    .WithOne(p => p.squad)
                    .HasForeignKey(p => p.SquadId);

            modelBuilder.Entity<Game>()
               .HasMany(g => g.Rules)
               .WithMany(r => r.Games)
               .UsingEntity(j =>
               {
                   j.ToTable("GameRule"); // Define the name of the join table
                   j.Property<int>("GameId"); // Define shadow property for GameId
                   j.Property<int>("RuleId"); // Define shadow property for RuleId
                   j.HasKey("GameId", "RuleId"); // Define the composite primary key
                   j.HasData(
                       new { GameId = 1, RuleId = 1 },
                       new { GameId = 2, RuleId = 2 },
                       new { GameId = 3, RuleId = 3 },
                       new { GameId = 4, RuleId = 4 }
                   );
               });









        }
    }
}
