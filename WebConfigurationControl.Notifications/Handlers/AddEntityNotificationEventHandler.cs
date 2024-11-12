using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using WebConfigurationControl.Infrastructure.Contracts;
using WebConfigurationControl.NotificationModels.Events;
using WebConfigurationControl.Notifications.Hubs;

namespace WebConfigurationControl.Notifications.Handlers
{
    public class AddEntityNotificationEventHandler : BaseNotificationHandler<AddEntityNotificationEventHandler>, INotificationHandler<EntityAddedEvent>
    {
        private const string MethodName = "ReceiveMessage";
        private readonly IEventNotificationRepository _eventNotificationRepository;

        public AddEntityNotificationEventHandler(IEventSubscriptionRepository eventSubscriptionRepository,
           IEventRepository eventRepository, IHubContext<EntityHub> entityHub, ILogger<AddEntityNotificationEventHandler> logger,
           IEventNotificationRepository eventNotificationRepository)
           : base(eventSubscriptionRepository, eventRepository, entityHub, logger)
        {
            _eventNotificationRepository = eventNotificationRepository;
        }

        public async Task Handle(EntityAddedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var eventNotification = await CreateEventNotification(notification.Message, notification.Type);

                await _eventNotificationRepository.Add(eventNotification);
                await _eventNotificationRepository.SaveChangesAsync(cancellationToken);

                var userIdsString = eventNotification.UserEventNotifications.Select(e => e.UserId.ToString()).ToList();

                await _entityHub.Clients.Users(userIdsString).SendAsync(MethodName, eventNotification.Id, eventNotification.Message, notification.Type, cancellationToken);

            } catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
