using Microsoft.EntityFrameworkCore;
using Podme.Domain.Entities;
using Podme.Infrastructure.Data;

namespace Podme.Infrastructure.Repositories
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(PodmeDbContext context) : base(context)
        {
        }

        public async Task<Subscription> GetByUserEmailAsync(string email)
        {
            return await _context.Subscriptions
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.User.Email == email);
        }

        public override async Task<Subscription> GetByIdAsync(Guid id)
        {
            return await _context.Subscriptions
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
