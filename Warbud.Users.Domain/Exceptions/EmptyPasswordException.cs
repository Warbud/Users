using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Domain.Exceptions
{
    public class EmptyPasswordException : WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EmptyPasswordException() : base("User password cannot be empty")
        {
        }
    }
}