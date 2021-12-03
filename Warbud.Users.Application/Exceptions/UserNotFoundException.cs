using System;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class UserNotFoundException : WarbudException
    {
        public Guid Id { get;}
        public UserNotFoundException(Guid id) : base($"User not found for id '{id}'.")
            => Id = id;
        
    }
}