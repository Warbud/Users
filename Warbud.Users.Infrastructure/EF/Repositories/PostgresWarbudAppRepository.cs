using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Users.Domain.Entities;
using Warbud.Users.Domain.Repositories;
using Warbud.Users.Domain.ValueObjects;
using Warbud.Users.Infrastructure.EF.Contexts;

namespace Warbud.Users.Infrastructure.EF.Repositories
{
    internal class PostgresWarbudAppRepository : IWarbudAppRepository
    {
        private readonly WriteDbContext _context;
        private readonly DbSet<WarbudApp> _apps;

        public PostgresWarbudAppRepository(WriteDbContext context)
        {
            _context = context;
            _apps = context.Apps;
        }

        public Task<WarbudApp> GetAsync(int id)
            => _apps.SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(WarbudApp app)
        {
            await _apps.AddAsync(app);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WarbudApp app)
        {
            _apps.Update(app);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(WarbudApp app)
        {
            _apps.Remove(app);
            await _context.SaveChangesAsync();
        }
    }
}