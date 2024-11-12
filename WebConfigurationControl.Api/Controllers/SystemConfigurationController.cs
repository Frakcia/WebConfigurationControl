using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebConfigurationControl.Application.Handlers.SystemConfiguration;

namespace WebConfigurationControl.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SystemConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Get([FromBody] GetSystemConfigurationsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] AddSystemConfigurationCommand command, CancellationToken cancellationToken)
        {
            _ = await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([FromBody] EditSystemConfigurationCommand command, CancellationToken cancellationToken)
        {
            _ = await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
