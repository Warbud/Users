using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class InvalidPasswordException : WarbudException
    {
        public InvalidPasswordException() : base("Invalid password")
        {
        }
    }
}