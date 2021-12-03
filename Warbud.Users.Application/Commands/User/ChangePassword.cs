using System;
using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Users.Application.Commands.User
{
    public record ChangePassword(Guid Id, string OldPassword, string NewPassword, string ConfirmPassword) : ICommand;
}