using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using WebConfigurationControl.Domain.Entities;
using WebConfigurationControl.Infrastructure.Contracts;
using WebConfigurationControl.Notifications.Hubs;

namespace WebConfigurationControl.Notifications.Handlers
{
    public abstract class BaseNotificationHandler<T>
    {
        protected readonly IEventSubscriptionRepository _eventSubscriptionRepository;
        protected readonly IEventRepository _eventRepository;
        protected readonly IHubContext<EntityHub> _entityHub;
        protected readonly ILogger<T> _logger;

        protected BaseNotificationHandler(IEventSubscriptionRepository eventSubscriptionRepository,
            IEventRepository eventRepository, IHubContext<EntityHub> entityHub, ILogger<T> logger)
        {
            _eventSubscriptionRepository = eventSubscriptionRepository;
            _eventRepository = eventRepository;
            _entityHub = entityHub;
            _logger = logger;
        }

        public async Task<EventNotification> CreateEventNotification(string message, Domain.Enums.EventType type)
        {
            var @event = await _eventRepository.GetByType(type);

            if(@event is null)
            {
                throw new Exception("Event was not found");
            }

            var eventSubscriptions = await _eventSubscriptionRepository.GetByEventId(@event.Id);
            var userIds = eventSubscriptions.Select(x => x.UserId).ToArray();

            var eventNotification = new EventNotification(message);

            var userEventNotification = userIds.Select(e => new UserEventNotification() { UserId = e, EventNotification = eventNotification }).ToList();

            eventNotification.UpdateEventNotifications(userEventNotification);

            return eventNotification;
        }
    }
}
