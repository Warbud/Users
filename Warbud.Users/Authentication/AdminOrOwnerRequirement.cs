using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Language;
using Microsoft.AspNetCore.Authorization;
using HotChocolate.Resolvers;
using Warbud.Shared.Abstraction.Constants;

namespace Warbud.Users.Authentication
{
    public class AdminOrOwnerRequirement : IAuthorizationRequirement { }
    
    public class AdminOrOwnerRequirementHandler : AuthorizationHandler<AdminOrOwnerRequirement, IResolverContext>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            AdminOrOwnerRequirement requirement,
            IResolverContext resource)
        {
            
            // if (resource.Operation.Operation == OperationType.Query)
            // {
            //     context.Succeed(requirement);
            //     return Task.CompletedTask;
            // }
            
            var role = context.User.FindFirst(x => x.Type == Claim.Name.Role)?.Value;
            if (role == Role.Name.Admin)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userIdAsString = context.User.FindFirst(x => x.Type == Claim.Name.Id)?.Value;
            if (string.IsNullOrWhiteSpace(userIdAsString)) return Task.CompletedTask;
            var userId = Guid.Parse(userIdAsString);

            switch (resource.Selection.SyntaxNode.Arguments[0].Value)
            {
                case StringValueNode stringValueNode:
                {
                    if (!Guid.TryParse(stringValueNode.Value, out var value))return Task.CompletedTask;
                    if (value == userId)
                    {
                        context.Succeed(requirement);
                    }
                    return Task.CompletedTask;
                }
                case ObjectValueNode objectValueNode:
                    var userIdAsObject = objectValueNode.Fields.First(x => x.Name.Value == "id").Value.Value;
                    if (!Guid.TryParse(userIdAsObject?.ToString(), out var result)) return Task.CompletedTask;
                    if (result != userId) return Task.CompletedTask;
                    context.Succeed(requirement);
                    return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}