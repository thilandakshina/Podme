using AutoMapper;
using MediatR;
using Podme.Application.Commands;
using Podme.Application.DTOs;
using Podme.Application.Exceptions;
using Podme.Domain.Entities;
using Podme.Infrastructure.Repositories;

namespace Podme.Application.Handlers
{
    public class StartSubscriptionCommandHandler : IRequestHandler<StartSubscriptionCommand, SubscriptionDto>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public StartSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUserRepository userRepository,
            IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<SubscriptionDto> Handle(StartSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var existingSubscription = await _subscriptionRepository.GetByUserEmailAsync(request.UserEmail);

            if (existingSubscription != null)
            {
                if (existingSubscription.Status == SubscriptionStatus.Active)
                {
                    throw new SubscriptionException($"User {request.UserEmail} already has an active subscription. Subscription id - {existingSubscription.Id}");
                }

                existingSubscription.Status = SubscriptionStatus.Active;
                existingSubscription.PausedDate = null;
                await _subscriptionRepository.UpdateAsync(existingSubscription);

                return _mapper.Map<SubscriptionDto>(existingSubscription);
            }

            var userInfo = await _userRepository.GetByEmailAsync(request.UserEmail);

            if (userInfo != null)
            {
                var subscription = new Subscription
                {
                    Id = Guid.NewGuid(),
                    Status = SubscriptionStatus.Active,
                    UserId = userInfo.Id,
                    StartDate = DateTime.UtcNow
                };
                await _subscriptionRepository.AddAsync(subscription);
                return _mapper.Map<SubscriptionDto>(subscription);
            }

            throw new SubscriptionException($"User {request.UserEmail} not exist");
        }
    }
}
