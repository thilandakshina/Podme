using Microsoft.EntityFrameworkCore;
using Podme.Domain.Entities;
using Podme.Infrastructure.Data;

namespace Podme.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(PodmeDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Subscription)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
