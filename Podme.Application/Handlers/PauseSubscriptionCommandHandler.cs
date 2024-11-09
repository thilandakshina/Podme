using AutoMapper;
using MediatR;
using Podme.Application.Commands;
using Podme.Application.DTOs;
using Podme.Application.Exceptions;
using Podme.Domain.Entities;
using Podme.Infrastructure.Repositories;

namespace Podme.Application.Handlers
{
    public class PauseSubscriptionCommandHandler : IRequestHandler<PauseSubscriptionCommand, SubscriptionDto>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public PauseSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<SubscriptionDto> Handle(PauseSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId);

            if (subscription == null)
            {
                throw new SubscriptionException($"Subscription with ID {request.SubscriptionId} not found");
            }

            if (subscription.Status == SubscriptionStatus.Paused)
            {
                throw new SubscriptionException("Subscription is already paused");
            }

            subscription.Status = SubscriptionStatus.Paused;
            subscription.PausedDate = DateTime.UtcNow;
            await _subscriptionRepository.UpdateAsync(subscription);

            return _mapper.Map<SubscriptionDto>(subscription);
        }
    }
}
