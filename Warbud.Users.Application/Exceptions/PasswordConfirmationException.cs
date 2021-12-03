using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class PasswordConfirmationException: WarbudException
    {
        public PasswordConfirmationException() : base("Password confirmation does not match")
        {
        }
    }
}