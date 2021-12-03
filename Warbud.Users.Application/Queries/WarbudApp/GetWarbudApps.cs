using System.Collections.Generic;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;

namespace Warbud.Users.Application.Queries.WarbudApp
{
    public record GetWarbudApps() : IQuery<IEnumerable<WarbudAppDto>>;
}