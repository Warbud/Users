using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Users.Application.Commands.WarbudApp
{
    public record AddWarbudApp(string AppName, string ModuleName): ICommand;
}