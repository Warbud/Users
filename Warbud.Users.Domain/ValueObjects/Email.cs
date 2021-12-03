using Warbud.Shared.Abstraction.Interfaces;
using Warbud.Users.Domain.Exceptions;

namespace Warbud.Users.Domain.ValueObjects
{
    public record Email : IValueType<string>
    {
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyEmailException();
            }
            Value = value;
        }
        public string Value { get; private set; }
        
        public static implicit operator string(Email email)
            => email.Value;
        
        public static implicit operator Email(string email)
            => new(email);
    }
}