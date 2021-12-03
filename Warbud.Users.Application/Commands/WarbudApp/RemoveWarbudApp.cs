using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Users.Application.Commands.WarbudApp
{
    public record RemoveWarbudApp(int Id) : ICommand;
}