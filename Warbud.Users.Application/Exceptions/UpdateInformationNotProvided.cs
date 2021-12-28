using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class UpdateInformationNotProvided : WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public UpdateInformationNotProvided() : base("Update information not provided")
        {
        }
    }
}