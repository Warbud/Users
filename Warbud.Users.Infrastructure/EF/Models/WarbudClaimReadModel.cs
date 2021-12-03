using System;

namespace Warbud.Users.Infrastructure.EF.Models
{
    internal class WarbudClaimReadModel
    {
        public Guid UserId { get; set; }
        public int AppId { get; set;}
        public int ProjectId { get; set;}
        public string Value { get; set; }
    }
}