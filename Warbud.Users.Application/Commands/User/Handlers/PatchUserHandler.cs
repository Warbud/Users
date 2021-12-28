using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Exceptions;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Application.Commands.User.Handlers
{
    public class PatchUserHandler: ICommandHandler<PatchUser>
    {
        private readonly IUserRepository _repository;
        private readonly IValidator<PatchUserDtoAggregate> _patchUserValidator;
        public PatchUserHandler(IUserRepository repository, IValidator<PatchUserDtoAggregate> patchUserValidator)
        {
            _repository = repository;
            _patchUserValidator = patchUserValidator;
        }

        public async Task HandleAsync(PatchUser command)
        {
            var (id, patchDoc) = command;
            if (!patchDoc.Operations.Any()) throw new UpdateInformationNotProvided();
            
            var user = await _repository.GetAsync(id);
            if (user is null) throw new UserNotFoundException(id);

            var userDto = user.AsPatchDto();
            patchDoc.ApplyTo(userDto);
            await _patchUserValidator.ValidateAndThrowAsync(new PatchUserDtoAggregate(id, userDto));

            user.UpdateEntity(userDto);
            await _repository.UpdateAsync(user);
        }
    }
}