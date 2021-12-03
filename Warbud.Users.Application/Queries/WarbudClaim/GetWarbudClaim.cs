using System;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;

namespace Warbud.Users.Application.Queries.WarbudClaim
{
    public record GetWarbudClaim(Guid UserId, int AppId, int ProjectId) : IQuery<WarbudClaimDto>;
}