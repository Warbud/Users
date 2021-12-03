using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;

namespace Warbud.Users.Application.Queries.WarbudApp
{
    public record GetWarbudApp(int Id) : IQuery<WarbudAppDto>;
}