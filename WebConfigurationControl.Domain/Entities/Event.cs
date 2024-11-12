using WebConfigurationControl.Domain.Enums;

namespace WebConfigurationControl.Domain.Entities
{
    public class Event : Entity
    {
        public string Name { get; set; }
        public EventType Type { get; set; }
        public bool IsDisabled { get; set; }
    }
}
