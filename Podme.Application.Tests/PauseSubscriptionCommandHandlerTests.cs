using AutoMapper;
using FluentAssertions;
using Moq;
using Podme.Application.Commands;
using Podme.Application.Exceptions;
using Podme.Application.Handlers;
using Podme.Application.Mappings;
using Podme.Domain.Entities;
using Podme.Infrastructure.Repositories;

namespace Podme.Application.Tests
{
    [TestFixture]
    public class PauseSubscriptionCommandHandlerTests
    {
        private Mock<ISubscriptionRepository> _mockSubscriptionRepo;
        private IMapper _mapper;
        private PauseSubscriptionCommandHandler _pauseHandler;

        [SetUp]
        public void Setup()
        {
            _mockSubscriptionRepo = new Mock<ISubscriptionRepository>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
            _pauseHandler = new PauseSubscriptionCommandHandler(_mockSubscriptionRepo.Object, _mapper);
        }

        [Test]
        public async Task Handle_AlreadyPausedSubscription_ShouldThrowException()
        {
            var subscriptionId = Guid.NewGuid();
            var command = new PauseSubscriptionCommand { SubscriptionId = subscriptionId };

            var subscription = new Subscription
            {
                Id = subscriptionId,
                Status = SubscriptionStatus.Paused,
                UserId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow.AddDays(-10),
                PausedDate = DateTime.UtcNow.AddDays(-1)
            };

            _mockSubscriptionRepo.Setup(x => x.GetByIdAsync(subscriptionId)).ReturnsAsync(subscription);

            var exception = await FluentActions.Invoking(() =>
            _pauseHandler.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<SubscriptionException>()
            .WithMessage("Subscription is already paused");
        }
    }
}
