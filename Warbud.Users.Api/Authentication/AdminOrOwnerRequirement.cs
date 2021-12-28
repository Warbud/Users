using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Interfaces;
using Warbud.Users.Api.Services;
using Warbud.Users.Domain.Entities;

namespace Warbud.Users.Api.Authentication
{
    public class AdminOrOwnerRequirement : IAuthorizationRequirement { }
    
    public class AdminOrOwnerRequirementHandler : AuthorizationHandler<AdminOrOwnerRequirement, User>
    {
        //TODO: change to service
        // private readonly IUserContextService _userService;
        // public AdminOrOwnerRequirementHandler(IUserContextService userService)
        // {
        //     _userService = userService;
        // }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            AdminOrOwnerRequirement requirement,
            User resource)
        {
            var role = context.User.FindFirst(x => x.Type == Claim.Name.Role)?.Value;
            var id = GetUserId(context);
            // var role = _userService.UserRole;
            // var id = _userService.UserId;
                
            if (id is null || id == Guid.Empty) return Task.CompletedTask;
            if (role != Role.Name.Admin && resource.Id.Value != id) return Task.CompletedTask;
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        private static Guid? GetUserId(AuthorizationHandlerContext context)
        {
            var id = context.User.FindFirst(x => x.Type == Claim.Name.Id)?.Value;
            if (id is null) return null;
            return Guid.Parse(id);
        }
    }
}