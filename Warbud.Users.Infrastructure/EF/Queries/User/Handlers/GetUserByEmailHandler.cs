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
    internal class GetUserByEmailHandler : IQueryHandler<GetUserByEmail, UserDto>
    {
        private readonly DbSet<UserReadModel> _users;

        public GetUserByEmailHandler(ReadDbContext context)
            => _users = context.Users;
        public Task<UserDto> HandleAsync(GetUserByEmail query)
            => _users
                .Where(u => u.Email == query.Email)
                .Select(x => x.AsDto())
                .AsNoTracking()
                .SingleOrDefaultAsync();
    }
}