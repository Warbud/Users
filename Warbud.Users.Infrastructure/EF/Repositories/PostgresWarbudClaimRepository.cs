using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Users.Domain.Entities;
using Warbud.Users.Domain.Repositories;
using Warbud.Users.Domain.ValueObjects;
using Warbud.Users.Infrastructure.EF.Contexts;

namespace Warbud.Users.Infrastructure.EF.Repositories
{
    internal class PostgresWarbudClaimRepository : IWarbudClaimRepository
    {
        private readonly WriteDbContext _context;
        private readonly DbSet<WarbudClaim> _claims;

        public PostgresWarbudClaimRepository(WriteDbContext context)
        {
            _context = context;
            _claims = context.Claims;
        }

        public Task<WarbudClaim> GetAsync(UserId userId, int appId, int projectId)
        => _claims.FirstOrDefaultAsync(c => c.UserId == userId && c.AppId == appId && c.ProjectId == projectId);

        public async Task AddAsync(WarbudClaim claim)
        {
            await _claims.AddAsync(claim);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WarbudClaim claim)
        {
            _claims.Update(claim);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(WarbudClaim claim)
        {
            _claims.Remove(claim);
            await _context.SaveChangesAsync();
        }
    }
}