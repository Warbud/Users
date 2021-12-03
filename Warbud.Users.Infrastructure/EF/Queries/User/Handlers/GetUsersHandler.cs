using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.User;
using Warbud.Users.Infrastructure.EF.Contexts;
using Warbud.Users.Infrastructure.EF.Models;

namespace Warbud.Users.Infrastructure.EF.Queries.User.Handlers
{
    internal class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
    {
        private readonly DbSet<UserReadModel> _users;

        public GetUsersHandler(ReadDbContext context)
            => _users = context.Users;

        public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
        {
            var dbQuery = _users
                .AsQueryable();
            
            return await dbQuery
                 .Select(x => x.AsDto())
                 .AsNoTracking()
                 .ToListAsync();
        }
    }
}