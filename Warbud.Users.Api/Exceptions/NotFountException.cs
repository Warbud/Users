using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Api.Exceptions
{
    public class NotFountException : WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public NotFountException() : base("Record not found")
        {
        }
    }
}