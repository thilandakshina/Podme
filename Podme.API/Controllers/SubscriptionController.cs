using MediatR;
using Microsoft.AspNetCore.Mvc;
using Podme.Application.Commands;
using Podme.Application.DTOs;

namespace Podme.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("start")]
        public async Task<ActionResult<SubscriptionDto>> StartSubscription([FromBody] StartSubscriptionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("pause/{subscriptionId:guid}")]
        public async Task<ActionResult<SubscriptionDto>> PauseSubscription(Guid subscriptionId)
        {
            var command = new PauseSubscriptionCommand { SubscriptionId = subscriptionId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
