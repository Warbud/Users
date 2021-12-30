using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class ClaimNotFoundException : WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound; 

        public ClaimNotFoundException() : base("Claim not found")
        {
        }
    }
}