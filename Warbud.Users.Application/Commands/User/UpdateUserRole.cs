using System;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Domain.Constants;

namespace Warbud.Users.Application.Commands.User
{
    /// <summary>
    /// Input for admin usage
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Role"></param>
    public record UpdateUserRole(Guid Id, Role Role) : ICommand, IInput<Guid>;
}