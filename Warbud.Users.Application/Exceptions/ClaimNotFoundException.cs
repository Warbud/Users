using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class ClaimNotFoundException : WarbudException
    {
        public ClaimNotFoundException() : base("Claim not found")
        {
        }
    }
}