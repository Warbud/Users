using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Infrastructure.Exceptions
{
    public class InvalidUserNameOrPasswordException: WarbudException
    {
        public InvalidUserNameOrPasswordException() : base("Invalid username or password")
        {
        }
    }
}