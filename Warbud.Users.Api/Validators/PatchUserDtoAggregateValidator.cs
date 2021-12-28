using FluentValidation;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Services;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Api.Validators
{
    public class PatchUserDtoAggregateValidator : AbstractValidator<PatchUserDtoAggregate>
    {
        public PatchUserDtoAggregateValidator(IUserReadService userReadService,
            IUserRepository userRepository)
        {
            RuleFor(input => input.PatchUserDto.FirstName)
                .MinimumLength(3)
                .NotEmpty();
            
            RuleFor(input => input.PatchUserDto.LastName)
                .MinimumLength(3)
                .NotEmpty();
            
            RuleFor(x => x.PatchUserDto.FirstName)
                .Must(name => !name.HasSpecialChars())
                .WithMessage("FirstName shouldn't contain any special character(s)");
            
            RuleFor(x => x.PatchUserDto.LastName)
                .Must(name => !name.HasSpecialChars())
                .WithMessage("LastName shouldn't contain any special character(s)");
            
            //TODO: add cancellation token
            RuleFor(input => input.PatchUserDto.Email)
                .NotEmpty()
                .EmailAddress();
                
            RuleFor(input => new {input.Id, input.PatchUserDto.Email}).CustomAsync(async (data, context, _) =>
            {
                var user = await userRepository.GetAsync(data.Id);
                if (await userReadService.ExistsByEmailAsync(data.Email) && user.Email != data.Email)
                {
                    context.AddFailure("Email", "There is already user with given email");
                }
            });
        }
    }
}