using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class EmailAlreadyInUseException : WarbudException
    {
        public string Email { get; }

        public EmailAlreadyInUseException(string email) : base($"User with email '{email}' already exists.")
        => Email = email;
        
    }
}