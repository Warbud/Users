using System;
using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class ClaimAlreadyInUseException : WarbudException
    {
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