using WebConfigurationControl.Application.Handlers.SystemConfiguration;

namespace WebConfigurationControl.Application.DTOs.SystemConfiguration.GetSystemConfigurationsQuery
{
    public class SystemConfigurationPageDto
    {
        public IEnumerable<SystemConfigurationDto> SystemConfigurations { get; set; }
        public long TotalCount { get; set; }
    }
}
