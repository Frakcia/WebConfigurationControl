using MediatR;

namespace WebConfigurationControl.NotificationModels.Events
{
    public class EntityChangedEvent : BaseEvent, INotification
    {
        public EntityChangedEvent(string name, Domain.Enums.EventType type)
            : base($"Конфигурация была обновлена({name})", type) { }
    }
}
