using System;
using System.Threading.Tasks;
using Warbud.Users.Application.DTO.External;
using Warbud.Users.Application.Services;

namespace Warbud.Users.Infrastructure.Services
{
    public sealed class FakeUserIdService : IIdService
    {
        public Task<UserIdDto> GenerateIdAsync()
            => Task.FromResult(new UserIdDto(Guid.NewGuid()));
    }
}