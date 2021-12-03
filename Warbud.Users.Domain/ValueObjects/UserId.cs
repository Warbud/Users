using System;
using Warbud.Shared.Abstraction.Interfaces;
using Warbud.Users.Domain.Exceptions;

namespace Warbud.Users.Domain.ValueObjects
{
    public record UserId : IValueType<Guid>
    {
        public Guid Value { get; private set; }

        public UserId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyUserIdException();
            }
            
            Value = value;
        }
        
        public static implicit operator Guid(UserId id)
            => id.Value;
        
        public static implicit operator UserId(Guid id)
            => new(id);
    }
}