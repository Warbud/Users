using Warbud.Shared.Abstraction.Interfaces;
using Warbud.Users.Domain.Exceptions;

namespace Warbud.Users.Domain.ValueObjects
{
    public record Password : IValueType<string>
    {
        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyPasswordException();
            }
            Value = value;
        }
        
        public static implicit operator string(Password email)
            => email.Value;
        
        public static implicit operator Password(string email)
            => new(email);

        public string Value { get; private set; }
    }
}