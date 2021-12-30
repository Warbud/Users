using FluentValidation;
using Warbud.Users.Application.Commands.User;
using Warbud.Users.Application.Services;

namespace Warbud.Users.Api.Validators
{
    public class AddUserValidator : AbstractValidator<AddUser>
    {
        public AddUserValidator(IUserReadService userReadService)
        {
            RuleFor(input => input.FirstName)
                .MinimumLength(3)
                .NotEmpty();
            
            RuleFor(input => input.LastName)
                .MinimumLength(3)
                .NotEmpty();
            
            RuleFor(x => x.FirstName)
                .Must(name => !name.HasSpecialChars())
                .WithMessage("FirstName shouldn't contain any special character(s)");
            
            RuleFor(x => x.LastName)
                .Must(name => !name.HasSpecialChars())
                .WithMessage("LastName shouldn't contain any special character(s)");

            RuleFor(input => input.Password)
                .MinimumLength(8);
            
            RuleFor(input => input.ConfirmPassword)
                .Equal(input => input.Password);

            //TODO: Add cancellation token
            RuleFor(input => input.Email)
                .NotEmpty()
                .EmailAddress()
                .CustomAsync(async (email, context, _) =>
                {
                    if (await userReadService.ExistsByEmailAsync(email))
                    {
                        context.AddFailure("Email", "There is already user with given email");
                    }
                });
        }
        
    }
}