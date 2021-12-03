using Warbud.Users.Domain.ValueObjects;

namespace Warbud.Users.Domain.Factories.WarbudClaim
{
    public interface IWarbudClaimFactory
    {
        Entities.WarbudClaim Create(UserId userId, int appName, int projectName, string value);
    }
}