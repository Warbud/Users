using System;

namespace Warbud.Users.Application.DTO
{
    public class WarbudClaimDto
    {
        public Guid UserId { get;  set; }
        public int AppId { get; set;}
        public int ProjectId { get; set;}
        public string Value { get; set; }
    }
}