using Microsoft.EntityFrameworkCore;
using Podme.Domain.Entities;

namespace Podme.Infrastructure.Data
{
    public class PodmeDbContext : DbContext
    {
        public PodmeDbContext(DbContextOptions<PodmeDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired();

                entity.HasOne(e => e.Subscription)
                      .WithOne(e => e.User)
                      .HasForeignKey<Subscription>(e => e.UserId);
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
            });

            // Seed Data
            var users = new List<User>
            {
                new User { Id = Guid.Parse("f8a1c92a-5465-4e6d-915f-222222d4f73a"), Email = "john.doe@gmail.com" , Name = "John Doe" },
                new User { Id = Guid.Parse("3a777c24-0c59-41c4-9b3f-7a423a5f2a5b"), Email = "jane.smith@gmail.com" , Name = "Jane Smith" },
                new User { Id = Guid.Parse("2b684731-f04b-4b26-a02b-13d93f2c7bc5"), Email = "william.johnson@gmail.com" , Name = "William Johnson" },
                new User { Id = Guid.Parse("d68b2d6e-cf4d-4f26-a7f4-f0e3f2ab7d58"), Email = "emily.davis@gmail.com" , Name = "Emily Davis" },
                new User { Id = Guid.Parse("b9b66ef8-647b-4e5c-b90f-1c17e9c5463e"), Email = "michael.brown@gmail.com" , Name = "Michael Brown" },
                new User { Id = Guid.Parse("f743ad35-a6d4-4a36-b8d2-16adf7f2d7ef"), Email = "sarah.martin@gmail.com" , Name = "Sarah Martin" },
                new User { Id = Guid.Parse("86f7e274-5a13-4c0d-980b-2fef4e2a9fc6"), Email = "daniel.garcia@gmail.com" , Name = "Daniel Garcia" },
                new User { Id = Guid.Parse("3f2d32c1-437b-4908-a4b1-e2b4f6b3a11f"), Email = "laura.jackson@gmail.com" , Name = "Laura Jackson" },
                new User { Id = Guid.Parse("715abcde-bb9d-49e8-830a-671d79a6c5b5"), Email = "james.lee@gmail.com" , Name = "James Lee" },
                new User { Id = Guid.Parse("e41234bc-9bdf-4377-b839-82e91f0c4db8"), Email = "olivia.miller@gmail.com" , Name = "Olivia Miller" }
            };

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
