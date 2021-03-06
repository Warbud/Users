using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class MissingUserIdException : WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public MissingUserIdException() : base($"Couldn't fetch id from external service.")
        {
        }
    }
}