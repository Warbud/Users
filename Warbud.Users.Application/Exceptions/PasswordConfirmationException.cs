using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class PasswordConfirmationException: WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public PasswordConfirmationException() : base("Password confirmation does not match")
        {
        }
    }
}