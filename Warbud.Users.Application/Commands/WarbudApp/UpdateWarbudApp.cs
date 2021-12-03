using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Users.Application.Commands.WarbudApp
{
#nullable enable
    public record UpdateWarbudApp(int Id, string? AppName = null, string? ModuleName = null): ICommand, IInput<int>;
#nullable disable
}