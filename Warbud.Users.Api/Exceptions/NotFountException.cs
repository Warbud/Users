using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Api.Exceptions
{
    public class NotFountException : WarbudException
    {
        public NotFountException() : base("Record not found")
        {
        }
    }
}