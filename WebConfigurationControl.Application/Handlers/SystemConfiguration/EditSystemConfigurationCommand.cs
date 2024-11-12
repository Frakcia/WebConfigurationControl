using MediatR;
using WebConfigurationControl.Domain.Enums;
using WebConfigurationControl.Infrastructure.Contracts;
using WebConfigurationControl.NotificationModels.Events;

namespace WebConfigurationControl.Application.Handlers.SystemConfiguration
{
    public class EditSystemConfigurationCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Settings { get; set; }
    }
    public class EditSystemConfigurationCommandHandler : IRequestHandler<EditSystemConfigurationCommand>
    {
        private readonly ISystemConfigurationRepository _systemConfigurationRepository;
        private readonly IMediator _mediator;

        public EditSystemConfigurationCommandHandler(ISystemConfigurationRepository systemConfigurationRepository, IMediator mediator)
        {
            _systemConfigurationRepository = systemConfigurationRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(EditSystemConfigurationCommand request, CancellationToken cancellationToken)
        {
            var systemConfig = await _systemConfigurationRepository.GetById(request.Id);

            if (systemConfig == null || systemConfig.IsDisabled)
            {
                throw new Exception($"SystemConfiguration with id:{request.Id} was not found");
            }

            if(systemConfig.Name == request.Name && systemConfig.Settings == request.Settings)
            {
                throw new Exception("SystemConfiguration has no changes");
            }

            systemConfig.IsDisabled = true;

            var newSystemConfig = new Domain.Entities.SystemConfiguration(
                name: request.Name, 
                type: systemConfig.Type,
                settings: request.Settings, 
                userId: systemConfig.UserId, 
                key: systemConfig.Key);

            await _systemConfigurationRepository.Add(newSystemConfig);

            await _systemConfigurationRepository.SaveChangesAsync(cancellationToken);

            _ = _mediator.Publish(new EntityChangedEvent(newSystemConfig.Name, EventType.ChangeSystemConfiguration));

            return Unit.Value;
        }
    }
}
