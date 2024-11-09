using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Podme.Infrastructure.Data;
using Podme.Infrastructure.Repositories;

namespace Podme.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<PodmeDbContext>(options =>
                options.UseInMemoryDatabase("PodmeDb"));

            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
