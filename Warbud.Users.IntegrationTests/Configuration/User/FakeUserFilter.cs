using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Warbud.Users.IntegrationTests.Configuration.User
{
    public class FakeUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var claimPrincipal = new ClaimsPrincipal();

            claimPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, TestConstants.FakeAdminId),
                    new Claim(ClaimTypes.Name, "Fake Admin"),
                    new Claim(ClaimTypes.Role, Shared.Abstraction.Constants.Role.Name.Admin)
                }));

            context.HttpContext.User = claimPrincipal;

            await next();
        }
    }
}