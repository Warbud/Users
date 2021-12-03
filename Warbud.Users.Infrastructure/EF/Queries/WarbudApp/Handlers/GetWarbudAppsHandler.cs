using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.WarbudApp;
using Warbud.Users.Infrastructure.EF.Contexts;
using Warbud.Users.Infrastructure.EF.Models;

namespace Warbud.Users.Infrastructure.EF.Queries.WarbudApp.Handlers
{
    internal class GetWarbudAppsHandler : IQueryHandler<GetWarbudApps, IEnumerable<WarbudAppDto>>
    {
        private readonly DbSet<WarbudAppReadModel> _apps;

        public GetWarbudAppsHandler(ReadDbContext context)
            => _apps = context.Apps;
        
        
        public async Task<IEnumerable<WarbudAppDto>> HandleAsync(GetWarbudApps query)
        {
            var dbQuery = _apps
                .AsQueryable();
            
            return await dbQuery
                .Select(x => x.AsDto())
                .AsNoTracking()
                .ToListAsync();
        }
    }
}