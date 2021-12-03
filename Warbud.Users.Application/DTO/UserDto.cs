using System;

namespace Warbud.Users.Application.DTO
{
    public class UserDto
    {
        public string Email { get; set; }
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Domain.Constants.Role Role { get; set; }
    }
}