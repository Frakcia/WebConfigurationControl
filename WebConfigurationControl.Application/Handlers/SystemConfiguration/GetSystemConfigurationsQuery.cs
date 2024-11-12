using Mapster;
using MediatR;
using WebConfigurationControl.Application.DTOs.SystemConfiguration.GetSystemConfigurationsQuery;
using WebConfigurationControl.Common.Models.Ordering;
using WebConfigurationControl.Common.Models.Pagination;
using WebConfigurationControl.Infrastructure.Contracts;

namespace WebConfigurationControl.Application.Handlers.SystemConfiguration
{
    public class GetSystemConfigurationsQuery : IRequest<SystemConfigurationPageDto>
    {
        public Pagination Pagination { get; set; }
        public IEnumerable<GlobalEntityOrdering> Ordering { get; set; }
    }

    public class GetSystemConfigurationsQueryHandler : IRequestHandler<GetSystemConfigurationsQuery, SystemConfigurationPageDto>
    {
        private readonly ISystemConfigurationRepository _systemConfigurationRepository;

        public GetSystemConfigurationsQueryHandler(ISystemConfigurationRepository systemConfigurationRepository)
        {
            _systemConfigurationRepository = systemConfigurationRepository;
        }

        public async Task<SystemConfigurationPageDto> Handle(GetSystemConfigurationsQuery request, CancellationToken cancellationToken)
        {
            var systemConfigurations = await _systemConfigurationRepository.GetAsync(request.Pagination, request.Ordering);

            var result = new SystemConfigurationPageDto();

            result.SystemConfigurations = systemConfigurations.Select(e => e.Adapt<SystemConfigurationDto>()).ToArray();
            result.TotalCount = await _systemConfigurationRepository.GetTotalCountAsync();

            return result;
        }
    }
}
