using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Users.Application.Services;
using Warbud.Users.Infrastructure.EF.Contexts;
using Warbud.Users.Infrastructure.EF.Models;

namespace Warbud.Users.Infrastructure.EF.Services
{
    internal sealed class PostgresUserReadService : IUserReadService
    {
        private readonly DbSet<UserReadModel> _users;

        public PostgresUserReadService(ReadDbContext context)
            => _users = context.Users;
        
        public Task<bool> ExistsByEmailAsync(string email)
            => _users.AnyAsync(u => u.Email == email);

        public Task<bool> ExistsByIdAsync(Guid id)
            => _users.AnyAsync(u => u.Id == id);
    }
}