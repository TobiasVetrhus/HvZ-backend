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
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Squad>()
                .HasMany(s => s.Players)
                .WithOne(p => p.Squad)
                .HasForeignKey(p => p.SquadId)
                .IsRequired(false);

            // Configure one-to-many relationship between Conversation and Message
            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Conversation)
                .HasForeignKey(m => m.ConversationId)
                .IsRequired(false);

            // Configure one-to-many relationship between User and Player
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Players)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired(false);

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
                .HasMany(p => p.Messages)
                .WithOne(m => m.Player)
                .HasForeignKey(m => m.PlayerId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Kills)
                .WithOne(k => k.Player)
                .HasForeignKey(k => k.PlayerId)
                .IsRequired(false);

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
                Title = "Cursed Asylum",
                Description = "Venture into the Cursed Asylum, where madness reigns and dark secrets lurk in every corner. Can you escape its horrifying grip?",
                GameState = GameStatus.Registration,
                PictureURL = "https://images.pexels.com/photos/2791657/pexels-photo-2791657.jpeg",
                MapURL = "assets/images/maps/Map1.png",
            },
            new Game
            {
                Id = 2,
                Title = "Eldritch Nightmare",
                Description = "Enter the Eldritch Nightmare, a realm of cosmic horrors and unspeakable terrors. Will you survive the relentless onslaught of the unknown?",
                GameState = GameStatus.InProgress,
                PictureURL = "https://images.pexels.com/photos/4997839/pexels-photo-4997839.jpeg",
                MapURL = "assets/images/maps/Map1.png",
            },
            new Game
            {
                Id = 3,
                Title = "Haunting Shadows",
                Description = "Face the Haunting Shadows, where vengeful spirits and malevolent entities lurk in the darkness. Can you escape their relentless pursuit?",
                GameState = GameStatus.InProgress,
                PictureURL = "https://images.pexels.com/photos/6770569/pexels-photo-6770569.jpeg",
                MapURL = "assets/images/maps/Map1.png",
            },
            new Game
            {
                Id = 4,
                Title = "Frostbitten Horror",
                Description = "Confront the Frostbitten Horror, a desolate wasteland where the cold bites deep and ancient evils stir. Will you survive its icy embrace?",
                GameState = GameStatus.Complete,
                PictureURL = "https://images.pexels.com/photos/6494658/pexels-photo-6494658.jpeg",
                MapURL = "assets/images/maps/Map1.png",
            },
            new Game
            {
                Id = 5,
                Title = "Dreaded Catacombs",
                Description = "Descend into the Dreaded Catacombs, a labyrinth of death and despair. Face the wrath of the undead in this chilling underground nightmare.",
                GameState = GameStatus.Registration,
                PictureURL = "https://images.pexels.com/photos/6495744/pexels-photo-6495744.jpeg",
                MapURL = "assets/images/maps/Map1.png",
            },
            new Game
            {
                Id = 6,
                Title = "Infernal Abyss",
                Description = "Plunge into the Infernal Abyss, a fiery inferno of torment and suffering. Survive the relentless flames or be consumed by the eternal blaze.",
                GameState = GameStatus.InProgress,
                PictureURL = "https://images.pexels.com/photos/7383327/pexels-photo-7383327.jpeg",
                MapURL = "assets/images/maps/Map1.png",
            },
            new Game
            {
                Id = 7,
                Title = "Phantom's Curse",
                Description = "Unravel the Phantom's Curse, a haunting enigma that threatens your very soul. Confront the ghostly apparitions and uncover their dark secrets.",
                GameState = GameStatus.InProgress,
                PictureURL = "https://images.pexels.com/photos/3772353/pexels-photo-3772353.jpeg",
                MapURL = "assets/images/maps/Map1.png",
            },
            new Game
            {
                Id = 8,
                Title = "Eternal Frostbite",
                Description = "Endure the Eternal Frostbite, where bone-chilling cold and frosty horrors await. Can you withstand the icy grip of this frozen nightmare?",
                GameState = GameStatus.Complete,
                PictureURL = "https://images.pexels.com/photos/6937710/pexels-photo-6937710.jpeg",
                MapURL = "assets/images/maps/Map1.png",
            },
            new Game
            {
                Id = 9,
                Title = "Sinister Hunt",
                Description = "Embark on the Sinister Hunt, a deadly game of survival where hunters become the hunted. Outwit your pursuers or meet a gruesome fate.",
                GameState = GameStatus.Registration,
                PictureURL = "https://images.pexels.com/photos/5976404/pexels-photo-5976404.jpeg",
                MapURL = "assets/images/maps/Map1.png",
            },
            new Game
            {
                Id = 10,
                Title = "Crimson Catastrophe",
                Description = "Face the Crimson Catastrophe, a world bathed in blood and chaos. Survive the crimson tide or drown in the sea of horror it brings.",
                GameState = GameStatus.InProgress,
                PictureURL = "https://images.pexels.com/photos/6495748/pexels-photo-6495748.jpeg",
                MapURL = "assets/images/maps/Map1.png",
            }

            );

            // Seed data for Kills
            modelBuilder.Entity<Kill>().HasData(
                new Kill { Id = 1, PlayerId = 5, Description = "Bitten in the shoulder by a relentless zombie, blood oozing from the wound, as they fought for their life.", TimeOfKill = DateTime.Now, LocationId = 11 },
                new Kill { Id = 2, PlayerId = 6, Description = "Caught off guard, player 6 was bitten on the leg, leaving a gruesome bite mark as a zombie overpowered them.", TimeOfKill = DateTime.Now, LocationId = 12 },
                new Kill { Id = 3, PlayerId = 7, Description = "In a frantic struggle, player 7 managed to fend off a biting zombie, but not before getting a nasty bite on their arm.", TimeOfKill = DateTime.Now, LocationId = 13 },
                new Kill { Id = 4, PlayerId = 8, Description = "Player 8 fought valiantly but was ultimately bitten on the neck, leaving a fatal wound in the heat of battle.", TimeOfKill = DateTime.Now, LocationId = 14 }
            );


            // Seed data for Locations
            modelBuilder.Entity<Location>().HasData(
                        new Location { Id = 1, XCoordinate = 23, YCoordinate = 56 },
                        new Location { Id = 2, XCoordinate = 89, YCoordinate = 1 },
                        new Location { Id = 3, XCoordinate = 45, YCoordinate = 78 },
                        new Location { Id = 4, XCoordinate = 87, YCoordinate = 43 },
                        new Location { Id = 5, XCoordinate = 34, YCoordinate = 92 },
                        new Location { Id = 6, XCoordinate = 12, YCoordinate = 65 },
                        new Location { Id = 7, XCoordinate = 76, YCoordinate = 29 },
                        new Location { Id = 8, XCoordinate = 50, YCoordinate = 14 },
                        new Location { Id = 9, XCoordinate = 69, YCoordinate = 37 },
                        new Location { Id = 10, XCoordinate = 31, YCoordinate = 49 },
                        new Location { Id = 11, XCoordinate = 38, YCoordinate = 61 },
                        new Location { Id = 12, XCoordinate = 72, YCoordinate = 23 },
                        new Location { Id = 13, XCoordinate = 51, YCoordinate = 47 },
                        new Location { Id = 14, XCoordinate = 29, YCoordinate = 84 },
                        new Location { Id = 15, XCoordinate = 17, YCoordinate = 39 },
                        new Location { Id = 16, XCoordinate = 93, YCoordinate = 12 },
                        new Location { Id = 17, XCoordinate = 64, YCoordinate = 58 },
                        new Location { Id = 18, XCoordinate = 45, YCoordinate = 75 },
                        new Location { Id = 19, XCoordinate = 81, YCoordinate = 32 },
                        new Location { Id = 20, XCoordinate = 22, YCoordinate = 69 }

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
                // Missions for Game 1
                new Mission { Id = 1, Name = "Supply Run", Description = "Gather supplies from the designated location.", LocationId = 1, GameId = 1 },
                new Mission { Id = 2, Name = "Zombie Hunt", Description = "Hunt down and eliminate zombie players.", LocationId = 2, GameId = 1 },
                new Mission { Id = 3, Name = "Survival Challenge", Description = "Test your survival skills in the wilderness.", LocationId = 3, GameId = 1 },

                // Missions for Game 2
                new Mission { Id = 4, Name = "Final Stand", Description = "Defend the safe zone from zombie attacks.", LocationId = 4, GameId = 2 },
                new Mission { Id = 5, Name = "Nocturnal Escape", Description = "Venture into the darkness on a Nocturnal Escape mission. Sneak past the undead and reach the designated safe zone undetected.", LocationId = 1, GameId = 2 },
                new Mission { Id = 6, Name = "Infected Research", Description = "Undertake the Infected Research mission to gather crucial data on the zombie virus. Stay alert, as the undead may guard their secrets.", LocationId = 2, GameId = 2 },

                // Missions for Game 3
                new Mission { Id = 7, Name = "Haunted Hideout", Description = "The Haunted Hideout mission challenges you to find refuge in an eerie, abandoned building. Can you withstand the supernatural forces within?", LocationId = 7, GameId = 3 },
                new Mission { Id = 8, Name = "Survivors' Revolt", Description = "Join the Survivors' Revolt mission and lead a group of determined survivors against the zombie horde. The fate of humanity hangs in the balance.", LocationId = 8, GameId = 3 },

                // Missions for Game 4
                new Mission { Id = 9, Name = "Epidemic Escape", Description = "In the Epidemic Escape mission, a virus outbreak threatens your group. Find a cure before it's too late, but beware of infected creatures.", LocationId = 9, GameId = 4 },
                new Mission { Id = 10, Name = "Crypt Crawl", Description = "Descend into the depths of darkness on a Crypt Crawl mission. Explore ancient catacombs, solve cryptic puzzles, and unearth hidden horrors.", LocationId = 10, GameId = 4 },

                // Missions for Game 5
                new Mission { Id = 11, Name = "The Abandoned Asylum", Description = "Enter the eerie and haunted abandoned asylum, and investigate the mysteries that lie within its walls.", LocationId = 11, GameId = 5 },
                new Mission { Id = 12, Name = "Curse of the Cursed", Description = "The Curse of the Cursed mission takes you to a cursed location where supernatural entities roam. Break the curse or become one of them.", LocationId = 12, GameId = 5 },

                // Missions for Game 6
                new Mission { Id = 13, Name = "Maze of Mirrors", Description = "Get lost in the Maze of Mirrors, where reality and illusion blend. Find the exit before the reflections turn against you.", LocationId = 13, GameId = 6 },
                new Mission { Id = 14, Name = "Realm of Shadows", Description = "Explore the mysterious Realm of Shadows, a dimension where darkness holds unimaginable secrets. Find your way back to reality.", LocationId = 14, GameId = 6 }
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
                new Squad { Id = 1, SquadName = "Alpha Squad", NumberOfMembers = 5, NumberOfDeceased = 0, GameId = 1 },
                new Squad { Id = 2, SquadName = "Zombie Horde", NumberOfMembers = 10, NumberOfDeceased = 3, GameId = 1 },
                new Squad { Id = 3, SquadName = "Survivor Team", NumberOfMembers = 7, NumberOfDeceased = 1, GameId = 1 },
                new Squad { Id = 4, SquadName = "Bravo Squad", NumberOfMembers = 6, NumberOfDeceased = 2, GameId = 1 },
                new Squad { Id = 5, SquadName = "Alpha Squad", NumberOfMembers = 4, NumberOfDeceased = 0, GameId = 2 },
                new Squad { Id = 6, SquadName = "Zombie Horde", NumberOfMembers = 9, NumberOfDeceased = 3, GameId = 2 }
            );

            //Seed Guid for Users
            var user1Guid = Guid.NewGuid();
            var user2Guid = Guid.NewGuid();
            var user3Guid = Guid.NewGuid();
            var user4Guid = Guid.NewGuid();
            var user5Guid = Guid.NewGuid();
            var user6Guid = Guid.NewGuid();
            var user7Guid = Guid.NewGuid();
            var user8Guid = Guid.NewGuid();

            // Seed data for Users
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser { Id = user1Guid, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com", Phone = "1234567890" },
                new AppUser { Id = user2Guid, FirstName = "Jane", LastName = "Smith", Email = "janesmith@example.com", Phone = "9876543210" },
                new AppUser { Id = user3Guid, FirstName = "Zombie", LastName = "Walker", Email = "zombiewalker@example.com", Phone = "5555555555" },
                new AppUser { Id = user4Guid, FirstName = "Player", LastName = "Four", Email = "playerfour@example.com", Phone = "1111111111" },
                new AppUser { Id = user5Guid, FirstName = "Sarah", LastName = "Williams", Email = "sarah@example.com", Phone = "2222222222" },
                new AppUser { Id = user6Guid, FirstName = "David", LastName = "Anderson", Email = "david@example.com", Phone = "3333333333" },
                new AppUser { Id = user7Guid, FirstName = "Linda", LastName = "Johnson", Email = "linda@example.com", Phone = "4444444444" },
                new AppUser { Id = user8Guid, FirstName = "Michael", LastName = "Brown", Email = "michael@example.com", Phone = "6666666666" }
            );


            // Seed data for Players
            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 1, Username = "Player1", Zombie = false, BiteCode = "BITE001", UserId = user1Guid, LocationId = 7, SquadId = 1, GameId = 1 },
                new Player { Id = 2, Username = "Player2", Zombie = false, BiteCode = "BITE002", UserId = user2Guid, LocationId = 8, SquadId = 2, GameId = 2 },
                new Player { Id = 3, Username = "Player3", Zombie = false, BiteCode = "BITE003", UserId = user3Guid, LocationId = 9, SquadId = 3, GameId = 3 },
                new Player { Id = 4, Username = "Player4", Zombie = false, BiteCode = "BITE004", UserId = user4Guid, LocationId = 10, SquadId = 4, GameId = 4 },
                new Player { Id = 5, Username = "Player5", Zombie = true, BiteCode = "ZOMBIE01", UserId = user5Guid, LocationId = 11, SquadId = 1, GameId = 1 },
                new Player { Id = 6, Username = "Player6", Zombie = true, BiteCode = "ZOMBIE02", UserId = user6Guid, LocationId = 12, SquadId = 2, GameId = 2 },
                new Player { Id = 7, Username = "Player7", Zombie = true, BiteCode = "ZOMBIE03", UserId = user7Guid, LocationId = 13, SquadId = 3, GameId = 2 },
                new Player { Id = 8, Username = "Player8", Zombie = true, BiteCode = "ZOMBIE04", UserId = user8Guid, LocationId = 14, SquadId = 4, GameId = 3 }
            );

            // Configure one-to-many relationship between Squad and Player
            modelBuilder.Entity<Squad>()
                    .HasMany(s => s.Players)
                    .WithOne(p => p.Squad)
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
