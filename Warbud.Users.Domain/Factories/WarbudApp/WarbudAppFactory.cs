namespace Warbud.Users.Domain.Factories.WarbudApp
{
    public sealed class WarbudAppFactory : IWarbudAppFactory
    {
        public Entities.WarbudApp Create(string appName, string moduleName)
            => new (appName, moduleName);
    }
}