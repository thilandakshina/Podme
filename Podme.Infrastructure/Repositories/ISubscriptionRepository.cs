using Podme.Domain.Entities;

namespace Podme.Infrastructure.Repositories
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<Subscription> GetByUserEmailAsync(string email);
    }
}
