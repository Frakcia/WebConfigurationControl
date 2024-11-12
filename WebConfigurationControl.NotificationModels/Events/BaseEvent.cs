using MediatR;
using WebConfigurationControl.Domain.Enums;

namespace WebConfigurationControl.NotificationModels.Events
{
    public abstract class BaseEvent
    {
        public BaseEvent(string message, EventType type)
        {
            Message = message;
            Type = type;
        }
        public string Message { get; }
        public EventType Type { get; }
    }
}
