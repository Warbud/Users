using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Shared.Abstraction;
using Warbud.Users.Domain.Entities;
using Warbud.Users.Infrastructure.EF.Configuration;

namespace Warbud.Users.Infrastructure.EF.Contexts
{
    internal sealed class WriteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<WarbudApp> Apps { get; set; }
        public DbSet<WarbudClaim> Claims { get; set; }

        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }
        
        public async Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && e.State is EntityState.Added or EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                ((AuditableEntity) entityEntry.Entity).LastModified = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                    ((AuditableEntity) entityEntry.Entity).Created = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("warbud.users");

            var configuration = new WriteConfiguration();
            modelBuilder.ApplyConfiguration<User>(configuration);
            modelBuilder.ApplyConfiguration<WarbudApp>(configuration);
            modelBuilder.ApplyConfiguration<WarbudClaim>(configuration);
        }
    }
}