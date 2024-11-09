using MediatR;
using Podme.Application.DTOs;

namespace Podme.Application.Commands
{
    public class PauseSubscriptionCommand : IRequest<SubscriptionDto>
    {
        public Guid SubscriptionId { get; set; }
    }
}
