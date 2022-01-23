using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Shared.Abstraction.Interfaces;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.User;
using Warbud.Users.Infrastructure.EF.Contexts;
using Warbud.Users.Infrastructure.EF.Models;
using Warbud.Users.Infrastructure.Exceptions;

namespace Warbud.Users.Infrastructure.EF.Queries.User.Handlers
{
    internal class QueryMeHandler : IQueryHandler<QueryMe, UserDto>
    {
        private readonly ICurrentUserService _userContextService;
        private readonly DbSet<UserReadModel> _users;

        public QueryMeHandler(ICurrentUserService userContextService, ReadDbContext context)
        {
            _userContextService = userContextService;
            _users = context.Users;
        }

        public async Task<UserDto> HandleAsync(QueryMe query)
        {
            var userId = _userContextService.UserId ?? throw new InvalidTokenException();
            return await _users
                .Where(u => u.Id == userId)
                .Select(x => x.AsDto())
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
    }
}