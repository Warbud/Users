using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.Exceptions;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Application.Commands.User.Handlers
{
    public class ChangePasswordHandler: ICommandHandler<ChangePassword>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;

        public ChangePasswordHandler(IUserRepository userRepository, IPasswordHasher<Domain.Entities.User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task HandleAsync(ChangePassword command)
        {
            var (guid, oldPassword, newPassword, confirmPassword) = command;
            if (newPassword != confirmPassword) throw new PasswordConfirmationException();

            var user = await  _userRepository.GetAsync(guid);
            if (user is null)
            {
                throw new UserNotFoundException(guid);
            }
            
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, oldPassword);
            if (result == PasswordVerificationResult.Failed) throw new InvalidPasswordException();
            
            var hashPassword = _passwordHasher.HashPassword(user, newPassword);
            user.SetPassword(hashPassword);
            await _userRepository.UpdateAsync(user);
        }
    }
}