using MediatR;
using Podme.Application.DTOs;

namespace Podme.Application.Commands
{
    public class StartSubscriptionCommand : IRequest<SubscriptionDto>
    {
        public string UserEmail { get; set; } 
    }
}
