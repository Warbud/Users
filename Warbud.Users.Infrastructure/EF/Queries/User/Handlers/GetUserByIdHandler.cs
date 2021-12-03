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
    internal class GetUserByIdHandler : IQueryHandler<GetUserById, UserDto>
    {
        private readonly DbSet<UserReadModel> _users;

        public GetUserByIdHandler(ReadDbContext context)
            => _users = context.Users;

        public Task<UserDto> HandleAsync(GetUserById query)
            => _users
                .Where(u => u.Id == query.Id)
                .Select(x => x.AsDto())
                .AsNoTracking()
                .SingleOrDefaultAsync();

    }
}