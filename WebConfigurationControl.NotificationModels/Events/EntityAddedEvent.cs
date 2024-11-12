using MediatR;

namespace WebConfigurationControl.NotificationModels.Events
{
    public class EntityAddedEvent : BaseEvent, INotification
    {
        public EntityAddedEvent(string name, Domain.Enums.EventType type)
            : base($"Конфигурация была добавлена({name})", type) { }
    }
}
