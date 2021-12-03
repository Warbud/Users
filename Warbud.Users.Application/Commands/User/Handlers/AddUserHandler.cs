using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.Exceptions;
using Warbud.Users.Application.Services;
using Warbud.Users.Domain.Factories.User;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Application.Commands.User.Handlers
{
    public class AddUserHandler : ICommandHandler<AddUser>
    {
        private readonly IUserRepository _repository;
        private readonly IUserFactory _factory;
        private readonly IIdService _idService;
        private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;
        public AddUserHandler(IUserRepository repository,
            IUserFactory factory,
            IIdService idService, IPasswordHasher<Domain.Entities.User> passwordHasher)
        {
            _repository = repository;
            _factory = factory;
            _idService = idService;
            _passwordHasher = passwordHasher;
        }
        
        public async Task HandleAsync(AddUser command)
        {
            var (email, password,firstName, lastName, _) = command;
            
            var userIdDto = await _idService.GenerateIdAsync();

            if (userIdDto is null)
            {
                throw new MissingUserIdException();
            }

            var user = _factory.Create(userIdDto.Id, email, password, firstName, lastName );
            var hashPassword = _passwordHasher.HashPassword(user, password);
            user.SetPassword(hashPassword);
            await _repository.AddAsync(user);
        }
    }
}