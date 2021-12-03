using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class AppNotFoundException : WarbudException
    {
        public AppNotFoundException() : base("App not found")
        {
        }
    }
}