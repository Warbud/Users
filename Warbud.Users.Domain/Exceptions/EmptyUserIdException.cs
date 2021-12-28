using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Domain.Exceptions
{
    public class EmptyUserIdException : WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EmptyUserIdException() : base("User ID cannot be empty")
        {
        }
    }
}