using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Warbud.Shared.Abstraction.Interfaces;
using Claim = Warbud.Shared.Abstraction.Constants.Claim;

namespace Warbud.Users.Api.Services
{
    

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public Guid? UserId
        {
            get
            {
                var id = User?.FindFirst(x => x.Type == Claim.Name.Id)?.Value;
                if (id is null) return null;
                return Guid.Parse(id);
            }
        }

        public string UserRole => User?.FindFirst(x => x.Type == Claim.Name.Role)?.Value;
    }
}