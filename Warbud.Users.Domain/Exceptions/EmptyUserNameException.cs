using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Domain.Exceptions
{
    public class EmptyUserNameException : WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EmptyUserNameException() : base("User name cannot be empty")
        {
        }
    }
}