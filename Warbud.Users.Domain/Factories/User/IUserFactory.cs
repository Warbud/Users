using Warbud.Users.Domain.ValueObjects;

namespace Warbud.Users.Domain.Factories.User
{
    public interface IUserFactory
    {
        Entities.User Create(UserId id, Email email, Password passwordHash, UserName firstName, UserName lastName);
    }
}