using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Domain.Exceptions
{
    public class EmptyUserNameException : WarbudException
    {
        public EmptyUserNameException() : base("User name cannot be empty")
        {
        }
    }
}