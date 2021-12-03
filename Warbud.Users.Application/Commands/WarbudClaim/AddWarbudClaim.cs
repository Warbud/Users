using System;
using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Users.Application.Commands.WarbudClaim
{
    public record AddWarbudClaim(Guid UserId, int AppId, int ProjectId, string Value) :ICommand;
}