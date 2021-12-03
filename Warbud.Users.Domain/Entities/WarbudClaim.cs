using System;
using Warbud.Shared.Interfaces;
using Warbud.Users.Domain.ValueObjects;

namespace Warbud.Users.Domain.Entities
{
    public class WarbudClaim : IEntity
    {
        private WarbudClaim(){}

        public WarbudClaim(UserId userId, int appId, int projectId, string value)
        {
            UserId = userId;
            AppId = appId;
            ProjectId = projectId;
            Value = value;
        }

        public UserId UserId { get; private set; }
        public int AppId { get;  private set;}
        public int ProjectId { get;  private set;}
        public string Value { get; set; }

#pragma warning disable 8632
        public override bool Equals(object? obj)
#pragma warning restore 8632
        {
            return obj is WarbudClaim claim && Equals(claim);
        }

        protected bool Equals(WarbudClaim other)
        {
            return UserId.Value.Equals(other.UserId.Value) && AppId == other.AppId && ProjectId == other.ProjectId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId.Value, AppId, ProjectId);
        }
    }
}