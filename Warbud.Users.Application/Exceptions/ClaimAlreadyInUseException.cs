using System;
using System.Net;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class ClaimAlreadyInUseException : WarbudException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public int AppName { get; }
        public int ProjectName { get; }
        public Guid UserId { get; }

        public ClaimAlreadyInUseException(Guid userId, int appName, int projectName) : base(
            $"Claim for '{userId}' in app '{appName}' for '{projectName}' already exists.")
        {
            AppName = appName;
            ProjectName = projectName;
            UserId = userId;
        }
        
    }
}