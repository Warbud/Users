using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class AppNotFoundException : WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public AppNotFoundException() : base("App not found")
        {
        }
    }
}