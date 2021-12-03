using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Users.Domain.Entities;
using Warbud.Users.Domain.Repositories;
using Warbud.Users.Domain.ValueObjects;
using Warbud.Users.Infrastructure.EF.Contexts;

namespace Warbud.Users.Infrastructure.EF.Repositories
{
    internal class PostgresUserRepository : IUserRepository
    {
        private readonly WriteDbContext _context;
        private readonly DbSet<User> _users;

        public PostgresUserRepository(WriteDbContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public Task<User> GetAsync(UserId id)
            => _users.SingleOrDefaultAsync(x => x.Id == id);

        public Task<User> GetAsync(Email email)
            => _users.SingleOrDefaultAsync(x => x.Email == email);

        public async Task AddAsync(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(User user)
        {
            _users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}