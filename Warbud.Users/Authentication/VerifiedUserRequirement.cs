using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Warbud.Shared.Abstraction.Constants;

namespace Warbud.Users.Authentication
{
    public class VerifiedUserRequirement : IAuthorizationRequirement { }
    
    public class VerifiedUserRequirementsHandler : AuthorizationHandler<VerifiedUserRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            VerifiedUserRequirement requirement)
        {
            var claim = context.User?.FindFirst(x => x.Type == Claim.Name.Role)?.Value;
            if (claim is Role.Name.Admin or Role.Name.BasicUser)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}