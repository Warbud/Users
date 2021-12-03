using System.Collections.Generic;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;

namespace Warbud.Users.Application.Queries.User
{
    public record GetUsers() : IQuery<IEnumerable<UserDto>>;
}