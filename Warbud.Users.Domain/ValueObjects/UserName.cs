using Warbud.Shared.Abstraction.Interfaces;
using Warbud.Users.Domain.Exceptions;

namespace Warbud.Users.Domain.ValueObjects
{
    public record UserName : IValueType<string>
    {
        public UserName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyUserNameException();
            }
            Value = value;
        }
        public string Value { get; private set; }
        
        public static implicit operator string(UserName userName)
            => userName.Value;
        
        public static implicit operator UserName(string userName)
            => new(userName);
    }
}