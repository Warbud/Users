using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Users.Application.Commands.User
{
    public record AddUser(string Email, string Password, string FirstName, string LastName, string ConfirmPassword) : ICommand;
}