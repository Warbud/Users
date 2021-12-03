using HotChocolate.AspNetCore.Authorization;
using Warbud.Shared.Abstraction;
using Warbud.Shared.Interfaces;
using Warbud.Users.Domain.Constants;
using Warbud.Users.Domain.ValueObjects;

namespace Warbud.Users.Domain.Entities
{
    public class User : AuditableEntity, IEntity
    {
        private User()
        {
            
        }
        
        public User(UserId id, Email email, Password password, UserName firstName, UserName lastName)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Role = Role.Viewer;
        }
        
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public UserId Id { get; init; }
        public UserName FirstName { get; private set; }
        public UserName LastName { get; private set; }
        
        [Authorize(Roles = new [] {Shared.Abstraction.Constants.Role.Name.Admin})]
        public Role Role { get; private set; } = Role.Viewer;
        
        public void SetPassword(Password password)
        {
            Password = password;
        }
    }
}