using System.Threading.Tasks;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.Exceptions;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Application.Commands.User.Handlers
{
    public class RemoveUserHandler :ICommandHandler<RemoveUser>
    {
        private readonly IUserRepository _repository;

        public RemoveUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(RemoveUser command)
        {
            var user = await _repository.GetAsync(command.Id);
            if (user is null)
            {
                throw new UserNotFoundException(command.Id);
            }
            await _repository.RemoveAsync(user);
        }
    }
}