using System;
using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Users.Application.Commands.WarbudClaim
{
    public record RemoveWarbudClaim(Guid UserId, int AppId, int ProjectId): ICommand;
}