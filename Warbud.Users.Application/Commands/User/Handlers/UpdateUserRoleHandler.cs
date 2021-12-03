using System;
using System.Threading.Tasks;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.Exceptions;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Application.Commands.User.Handlers
{
    public class UpdateUserRoleHandler : ICommandHandler<UpdateUserRole>
    {
        private readonly IUserRepository _repository;

        public UpdateUserRoleHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        
        public async Task HandleAsync(UpdateUserRole command)
        {
            var user = await _repository.GetAsync(command.Id);
            if (user is null)
            {
                throw new UserNotFoundException(command.Id);
            }

            user.UpdateEntity(command);
            await _repository.UpdateAsync(user);
        }
    }
}