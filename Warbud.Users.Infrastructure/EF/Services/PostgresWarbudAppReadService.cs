using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Users.Application.Services;
using Warbud.Users.Infrastructure.EF.Contexts;
using Warbud.Users.Infrastructure.EF.Models;

namespace Warbud.Users.Infrastructure.EF.Services
{
    internal sealed class PostgresWarbudAppReadService : IWarbudAppReadService
    {
        private readonly DbSet<WarbudAppReadModel> _apps;

        public PostgresWarbudAppReadService(ReadDbContext context)
            => _apps = context.Apps;
        
        public Task<bool> ExistsAsync(string appName, string moduleName)
            => _apps.AnyAsync(u => u.AppName == appName && u.ModuleName == moduleName);

        public Task<bool> ExistsAsync(int id)
            => _apps.AnyAsync(u => u.Id == id);
    }
}