using WebConfigurationControl.Domain.Enums;

namespace WebConfigurationControl.Domain.Entities
{
    public class SystemConfiguration : Entity
    {
        private SystemConfiguration() { }

        public SystemConfiguration(string name, SystemConfigutationType type, string settings, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            Settings = settings;
            UserId = userId;
            Key = Guid.NewGuid();
            CreatedAtUtc = DateTime.UtcNow;
        }

        public SystemConfiguration(string name, SystemConfigutationType type, string settings, Guid userId, Guid key)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            Settings = settings;
            UserId = userId;
            Key = key;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public string Name { get; set; }
        public Guid Key { get; set; }
        public Guid UserId { get; set; }
        public SystemConfigutationType Type { get; set; }
        public string Settings { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public bool IsDisabled { get; set; }
    }
}
