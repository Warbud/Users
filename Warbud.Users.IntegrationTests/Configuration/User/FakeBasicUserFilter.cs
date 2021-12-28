using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Warbud.Users.IntegrationTests.Configuration.User
{
    public class FakeBasicUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var claimPrincipal = new ClaimsPrincipal();

            claimPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, TestConstants.FakeBasicId),
                    new Claim(ClaimTypes.Name, "Fake Basic User"),
                    new Claim(ClaimTypes.Role, Shared.Abstraction.Constants.Role.Name.BasicUser)
                }));

            context.HttpContext.User = claimPrincipal;
            await next();
        }
    }
}