using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.WarbudApp;
using Warbud.Users.Application.Queries.WarbudClaim;
using Warbud.Users.Infrastructure.EF.Contexts;
using Warbud.Users.Infrastructure.EF.Models;

namespace Warbud.Users.Infrastructure.EF.Queries.WarbudClaim.Handlers
{
    internal class GetWarbudClaimsByUserIdHandler : IQueryHandler<GetWarbudClaimsByUserId, IEnumerable<WarbudClaimDto>>
    {
        private readonly DbSet<WarbudClaimReadModel> _claims;

        public GetWarbudClaimsByUserIdHandler(ReadDbContext context)
            => _claims = context.Claims;
        
        public async Task<IEnumerable<WarbudClaimDto>> HandleAsync(GetWarbudClaimsByUserId query)
        {
            var dbQuery = _claims
                .AsQueryable();
            
            return await dbQuery
                .Where(x => x.UserId == query.UserId)
                .Select(x => x.AsDto())
                .AsNoTracking()
                .ToListAsync();
        }
    }
}