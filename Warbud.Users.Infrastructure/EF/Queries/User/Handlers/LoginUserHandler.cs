using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Shared.Options;
using Warbud.Users.Application.Queries.User;
using Warbud.Users.Domain.Repositories;
using Warbud.Users.Infrastructure.EF.Options;
using Warbud.Users.Infrastructure.Exceptions;

namespace Warbud.Users.Infrastructure.EF.Queries.User.Handlers
{
    public class LoginUserHandler : IQueryHandler<LoginUser, string>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public LoginUserHandler(IUserRepository repository,
            AuthenticationSettings authenticationSettings,
            IPasswordHasher<Domain.Entities.User> passwordHasher)
        {
            _repository = repository;
            _authenticationSettings = authenticationSettings;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> HandleAsync(LoginUser query)
        {
            var (email, password) = query;
            var user = await _repository.GetAsync(email);
            if (user is null) throw new InvalidUserNameOrPasswordException();
            
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Failed) throw new InvalidUserNameOrPasswordException();

            var claims = new List<System.Security.Claims.Claim>
            {
                new(Claim.Name.Id, user.Id.Value.ToString()),
                new(Claim.Name.Role, $"{user.Role.ToString()}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddSeconds(_authenticationSettings.JwtExpireSeconds);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}