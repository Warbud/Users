using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Domain.Exceptions
{
    public class EmptyUserIdException : WarbudException
    {
        public EmptyUserIdException() : base("User ID cannot be empty")
        {
        }
    }
}