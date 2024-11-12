using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebConfigurationControl.Domain.Entities;

namespace WebConfigurationControl.Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DbConnection");
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEventNotification>()
                .HasKey(e => new { e.EventNotificationId, e.UserId });

            modelBuilder.Entity<UserEventNotification>()
                .HasOne(e => e.User)
                .WithMany(e=> e.UserEventNotifications)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<UserEventNotification>()
               .HasOne(e => e.EventNotification)
               .WithMany(e => e.UserEventNotifications)
               .HasForeignKey(e => e.EventNotificationId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Subscriptions)
                .WithOne(e=> e.User)
                .HasForeignKey(e=> e.UserId);

            modelBuilder.Entity<EventSubscription>()
                .HasOne(e => e.Event)
                .WithMany()
                .HasForeignKey(e=> e.EventId);

            modelBuilder.Entity<User>()
                .HasData( 
                new User() { Id = Guid.Parse("8ca40b6e-d767-452f-b71d-59627946b7ae"), Name = "User1" }
                );

            modelBuilder.Entity<Event>()
                .HasData(
                new Event() { Id = Guid.Parse("c931d0cb-f1c5-48b4-9d23-0c806b970980"), Name = "Add systemConfig", Type = Domain.Enums.EventType.AddSystemConfiguration },
                new Event() { Id = Guid.Parse("bf9e94e8-81e3-48a7-8859-bec574c6d7de"), Name = "Change systemConfig", Type = Domain.Enums.EventType.ChangeSystemConfiguration }
                );
        }

        public DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSubscription> EventSubscriptions { get; set; }
        public DbSet<EventNotification> EventNotifications { get; set; }
        public DbSet<UserEventNotification> UserEventNotifications { get; set; }
    }
}
