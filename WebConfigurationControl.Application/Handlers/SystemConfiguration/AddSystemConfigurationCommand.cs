using MediatR;
using WebConfigurationControl.Domain.Enums;
using WebConfigurationControl.Infrastructure.Contracts;
using WebConfigurationControl.NotificationModels.Events;

namespace WebConfigurationControl.Application.Handlers.SystemConfiguration
{
    public class AddSystemConfigurationCommand : IRequest
    {
        public string Name { get; set; }
        public SystemConfigutationType Type { get; set; }
        public string Settings { get; set; }
        public Guid UserId { get; set; }
    }

    public class AddSystemConfigurationCommandHandler : IRequestHandler<AddSystemConfigurationCommand>
    {
        private readonly ISystemConfigurationRepository _systemConfigurationRepository;
        private readonly IMediator _mediator;

        public AddSystemConfigurationCommandHandler(ISystemConfigurationRepository systemConfigurationRepository, IMediator mediator)
        {
            _systemConfigurationRepository = systemConfigurationRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(AddSystemConfigurationCommand request, CancellationToken cancellationToken)
        {
            var existNameConfiguration = await _systemConfigurationRepository.GetByNameAsync(request.Name, request.UserId);

            if (existNameConfiguration != null)
            {
                throw new Exception("Configuration with same name already exist");
            }

            var newConfiguration = new Domain.Entities.SystemConfiguration(request.Name, request.Type, request.Settings, request.UserId);

            await _systemConfigurationRepository.Add(newConfiguration);
            await _systemConfigurationRepository.SaveChangesAsync(cancellationToken);

            _ = _mediator.Publish(new EntityAddedEvent(newConfiguration.Name, EventType.AddSystemConfiguration));

            return Unit.Value;
        }
    }
}
