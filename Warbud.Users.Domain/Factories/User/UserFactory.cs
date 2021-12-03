using Warbud.Users.Domain.ValueObjects;

namespace Warbud.Users.Domain.Factories.User
{
    public sealed class UserFactory : IUserFactory
    {

        public Entities.User Create(UserId id, Email email, Password passwordHash, UserName firstName, UserName lastName)
            => new (id, email, passwordHash, firstName, lastName);
    }
}