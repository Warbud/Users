using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Infrastructure.Exceptions
{
    public class InvalidTokenException: WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public InvalidTokenException() : base("Invalid token")
        {
        }
    }
}