using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.WarbudApp;
using Warbud.Users.Infrastructure.EF.Contexts;
using Warbud.Users.Infrastructure.EF.Models;
using System.Linq;

namespace Warbud.Users.Infrastructure.EF.Queries.WarbudApp.Handlers
{
    internal class GetWarbudAppHandler: IQueryHandler<GetWarbudApp, WarbudAppDto>
    {
        private readonly DbSet<WarbudAppReadModel> _apps;

        public GetWarbudAppHandler(ReadDbContext context)
            => _apps = context.Apps;
        
        public Task<WarbudAppDto> HandleAsync(GetWarbudApp query)
            => _apps
                .Where(u => u.Id == query.Id)
                .Select(x => x.AsDto())
                .AsNoTracking()
                .SingleOrDefaultAsync();
    }
}