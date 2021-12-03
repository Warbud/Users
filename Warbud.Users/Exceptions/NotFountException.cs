using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Exceptions
{
    public class NotFountException : WarbudException
    {
        public NotFountException() : base("Record not found")
        {
        }
    }
}