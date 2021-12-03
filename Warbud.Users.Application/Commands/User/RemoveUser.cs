using System;
using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Users.Application.Commands.User
{
    public record RemoveUser(Guid Id) : ICommand;
}