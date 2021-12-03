using FluentValidation;
using Warbud.Users.Application.Commands.WarbudApp;

namespace Warbud.Users.Validators
{
    public class WarbudAppInputValidator : AbstractValidator<AddWarbudApp>
    {
        public WarbudAppInputValidator()
        {
            RuleFor(input => input.AppName)
                .NotEmpty()
                .NotNull()
                .Length(2, 50);
            
            RuleFor(input => input.ModuleName)
                .NotEmpty()
                .NotNull()
                .Length(2, 50);
        }
    }
}