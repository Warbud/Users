using Microsoft.EntityFrameworkCore;
using Warbud.Users.Infrastructure.EF.Configuration;
using Warbud.Users.Infrastructure.EF.Models;

namespace Warbud.Users.Infrastructure.EF.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<WarbudAppReadModel> Apps { get; set; }
        public DbSet<WarbudClaimReadModel> Claims { get; set; }

        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("warbud.users");

            var configuration = new ReadConfiguration();
            modelBuilder.ApplyConfiguration<UserReadModel>(configuration);
            modelBuilder.ApplyConfiguration<WarbudAppReadModel>(configuration);
            modelBuilder.ApplyConfiguration<WarbudClaimReadModel>(configuration);
        }
    }
}