using Warbud.Users.Domain.ValueObjects;

namespace Warbud.Users.Domain.Factories.WarbudClaim
{
    public sealed class WarbudClaimFactory : IWarbudClaimFactory
    {
        public Entities.WarbudClaim Create(UserId userId, int appName, int projectName, string value)
            => new (userId, appName, projectName, value);
    }
}