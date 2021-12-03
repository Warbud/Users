using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Domain.Exceptions
{
    public class EmptyEmailException : WarbudException
    {
        public EmptyEmailException() : base("User email cannot be empty")
        {
        }
    }
}