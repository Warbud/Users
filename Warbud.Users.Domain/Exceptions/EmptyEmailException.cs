using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Domain.Exceptions
{
    public class EmptyEmailException : WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EmptyEmailException() : base("User email cannot be empty")
        {
        }
    }
}