using System;
using System.Collections.Generic;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;

namespace Warbud.Users.Application.Queries.WarbudClaim
{
    public record GetWarbudClaimsByUserId(Guid UserId) : IQuery<IEnumerable<WarbudClaimDto>>;
}