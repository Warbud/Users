using Warbud.Users.Domain.ValueObjects;

namespace Warbud.Users.Domain.Factories.WarbudApp
{
    public interface IWarbudAppFactory
    {
        Entities.WarbudApp Create(string appName, string moduleName);
    }
}