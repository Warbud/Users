using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;

namespace Warbud.Users.Application.Queries.User
{
    public record GetUserByEmail(string Email) : IQuery<UserDto>;
}