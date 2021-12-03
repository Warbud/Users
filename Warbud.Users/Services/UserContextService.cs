using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Claim = Warbud.Shared.Abstraction.Constants.Claim;

namespace Warbud.Users.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        Guid? GetUserId { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public Guid? GetUserId =>
            User is null ? null : Guid.Parse(User.FindFirst(x => x.Type == Claim.Name.Id)?.Value);
    }
}