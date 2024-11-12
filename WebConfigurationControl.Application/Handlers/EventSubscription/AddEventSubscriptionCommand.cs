using MediatR;
using WebConfigurationControl.Infrastructure.Contracts;

namespace WebConfigurationControl.Application.Handlers.EventSubscription
{
    public class AddEventSubscriptionCommand : IRequest
    {
        public Guid UserId { get; set; }
        public IEnumerable<Guid> EventIds { get; set; } = Array.Empty<Guid>();
    }

    public class AddEventSubscriptionCommandHandler : IRequestHandler<AddEventSubscriptionCommand>
    {
        private readonly IEventSubscriptionRepository _eventSubscriptionRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public AddEventSubscriptionCommandHandler(IEventSubscriptionRepository eventSubscriptionRepository, 
            IEventRepository eventRepository, IUserRepository userRepository)
        {
            _eventSubscriptionRepository = eventSubscriptionRepository;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(AddEventSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId);

            if (user == null)
            {
                throw new Exception($"User with id:{request.UserId} was not found");
            }

            var events = await _eventRepository.GetByIds(request.EventIds);

            var eventIds = events.Select(e => e.Id).ToArray();
            var existedSubscriptions = await _eventSubscriptionRepository.GetByUserIdAndEventIds(request.UserId, eventIds);

            var eventSubscriptions = events
                .Where(e=> !existedSubscriptions.Any(x=> x.EventId == e.Id))
                .Select(e=> new Domain.Entities.EventSubscription(eventId: e.Id, userId: request.UserId))
                .ToArray();

            if(eventSubscriptions.Length != 0)
            {
                await _eventSubscriptionRepository.AddRange(eventSubscriptions);
                await _eventSubscriptionRepository.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
