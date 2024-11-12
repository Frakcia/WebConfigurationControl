using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebConfigurationControl.Application.Handlers.EventSubscription;

namespace WebConfigurationControl.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventSubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventSubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Subscribe([FromBody] AddEventSubscriptionCommand command, CancellationToken cancellationToken)
        {
            _ = await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
