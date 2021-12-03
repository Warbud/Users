using FluentValidation;
using Warbud.Users.Application.Commands.User;
using Warbud.Users.Application.Services;

namespace Warbud.Users.Validators
{
    public class UserInputValidator : AbstractValidator<AddUser>
    {
        public UserInputValidator(IUserReadService userReadService)
        {
            RuleFor(input => input.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(input => input.Password)
                .MinimumLength(8);

            RuleFor(input => input.ConfirmPassword)
                .Equal(input => input.Password);

            RuleFor(input => input.Email).Custom((email, context) =>
            {
                if (userReadService.ExistsByEmailAsync(email).Result)
                {
                    context.AddFailure("Email", "There is already user with given email");
                }
            });
        }
    }
}