using Warbud.Shared.Abstraction.Queries;

namespace Warbud.Users.Application.Queries.User
{
    public record LoginUser(string Email, string Password) : IQuery<string>;
}