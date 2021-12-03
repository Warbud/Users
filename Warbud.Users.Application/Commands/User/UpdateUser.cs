using System;
using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Users.Application.Commands.User
{
    /// <summary>
    /// Input for user personal information
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
#nullable enable
    public record UpdateUser(Guid Id, string? FirstName = null, string? LastName = null, string? Email = null) : ICommand, IInput<Guid>;
#nullable disable
}