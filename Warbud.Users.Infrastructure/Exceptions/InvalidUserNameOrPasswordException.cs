using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Infrastructure.Exceptions
{
    public class InvalidUserNameOrPasswordException: WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public InvalidUserNameOrPasswordException() : base("Invalid username or password")
        {
        }
    }
}