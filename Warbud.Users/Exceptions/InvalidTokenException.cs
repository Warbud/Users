using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Exceptions
{
    public class InvalidTokenException: WarbudException
    {
        public InvalidTokenException() : base("Invalid token")
        {
        }
    }
}