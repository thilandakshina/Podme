using AutoMapper;
using FluentAssertions;
using Moq;
using Podme.Application.Commands;
using Podme.Application.Handlers;
using Podme.Application.Mappings;
using Podme.Domain.Entities;
using Podme.Infrastructure.Repositories;

namespace Podme.Application.Tests
{
    [TestFixture]
    public class StartSubscriptionCommandHandlerTests
    {
        private Mock<ISubscriptionRepository> _mockSubscriptionRepo;
        private Mock<IUserRepository> _mockUserRepo;
        private IMapper _mapper;
        private StartSubscriptionCommandHandler _startHandler;

        [SetUp]
        public void Setup()
        {
            _mockSubscriptionRepo = new Mock<ISubscriptionRepository>();
            _mockUserRepo = new Mock<IUserRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _startHandler = new StartSubscriptionCommandHandler(_mockSubscriptionRepo.Object, _mockUserRepo.Object, _mapper);
        }

        [Test]
        public async Task Handle_NewUser_WithNoExistingSubscription_ShouldCreateNewSubscription()
        {
            var userEmail = "testUser1@gmail.com";
            var userId = Guid.NewGuid();
            var command = new StartSubscriptionCommand { UserEmail = userEmail };
            
            var user = new User { Id = userId, Email = userEmail };
            
            _mockSubscriptionRepo.Setup(x => x.GetByUserEmailAsync(userEmail))
                .ReturnsAsync(() => null);

            _mockUserRepo.Setup(x => x.GetByEmailAsync(userEmail))
                .ReturnsAsync(user);

            var result = await _startHandler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Status.Should().Be(SubscriptionStatus.Active);
            
            _mockSubscriptionRepo.Verify(x => x.AddAsync(It.Is<Subscription>(s => 
                s.UserId == userId && 
                s.Status == SubscriptionStatus.Active)), 
                Times.Once);
        }
    }
}
