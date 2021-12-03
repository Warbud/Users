using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.WarbudClaim;
using Warbud.Users.Infrastructure.EF.Contexts;
using Warbud.Users.Infrastructure.EF.Models;
using System.Linq;

namespace Warbud.Users.Infrastructure.EF.Queries.WarbudClaim.Handlers
{
    internal class GetWarbudClaimHandler: IQueryHandler<GetWarbudClaim, WarbudClaimDto>
    {
        private readonly DbSet<WarbudClaimReadModel> _claims;

        public GetWarbudClaimHandler(ReadDbContext context)
            => _claims = context.Claims;
        
        public Task<WarbudClaimDto> HandleAsync(GetWarbudClaim query)
            => _claims
                .Where(u => u.UserId == query.UserId && u.AppId == query.AppId && u.ProjectId == query.ProjectId)
                .Select(x => x.AsDto())
                .AsNoTracking()
                .SingleOrDefaultAsync();
    }
}