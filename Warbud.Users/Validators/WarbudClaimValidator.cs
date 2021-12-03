using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Users.Application.Commands.WarbudClaim;
using Warbud.Users.Application.Services;

namespace Warbud.Users.Validators
{
    public class WarbudClaimInputValidator : AbstractValidator<AddWarbudClaim>
    {
        public WarbudClaimInputValidator(IUserReadService userReadService, IWarbudAppReadService appReadService)
        {
            RuleFor(input => input.UserId).Custom((id, context) =>
            {
                if (!userReadService.ExistsByIdAsync(id).Result)
                {
                    context.AddFailure("UserId", "There is no user with given id");
                }
            });
            
            RuleFor(input => input.AppId).Custom((id, context) =>
            {
                if (!appReadService.ExistsAsync(id).Result)
                {
                    context.AddFailure("AppId", "There is no app with given id");
                }
            });
            
            
           RuleFor(input => input.Value).NotEmpty().NotNull().Length(2, 50).Custom((value, context) =>
           {
               var claimValue = Claim.Value.GetValueList();

               if (claimValue.All(x => x != value))
               {
                   context.AddFailure("Value", "Invalid claim value");
               }
           });
        }
    }
}