using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Users.Application.Services;
using Warbud.Users.Infrastructure.EF.Contexts;
using Warbud.Users.Infrastructure.EF.Models;

namespace Warbud.Users.Infrastructure.EF.Services
{
    internal sealed class PostgresWarbudClaimReadService : IWarbudClaimReadService
    {
        private readonly DbSet<WarbudClaimReadModel> _claims;

        public PostgresWarbudClaimReadService(ReadDbContext context)
            => _claims = context.Claims;

        public Task<bool> ExistsByKeyAsync(Guid userId, int appId, int projectId)
            => _claims.AnyAsync(u => u.UserId == userId && u.AppId == appId && u.ProjectId == projectId);
    }
}