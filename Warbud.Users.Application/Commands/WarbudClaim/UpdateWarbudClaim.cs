using System;
using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Users.Application.Commands.WarbudClaim
{
    public record UpdateWarbudClaim(Guid UserId, int AppId, int ProjectId, string Value): ICommand;
}