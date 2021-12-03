using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Domain.Exceptions
{
    public class EmptyPasswordException : WarbudException
    {
        public EmptyPasswordException() : base("User password cannot be empty")
        {
        }
    }
}