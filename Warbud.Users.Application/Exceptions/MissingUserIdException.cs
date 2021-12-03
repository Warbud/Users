using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class MissingUserIdException : WarbudException
    {
        public MissingUserIdException() : base($"Couldn't fetch id from external service.")
        {
        }
    }
}