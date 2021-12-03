using System.Threading.Tasks;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.Exceptions;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Application.Commands.WarbudClaim.Handlers
{
    public class RemoveWarbudClaimHandler : ICommandHandler<RemoveWarbudClaim>
    {
        private readonly IWarbudClaimRepository _repository;

        public RemoveWarbudClaimHandler(IWarbudClaimRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(RemoveWarbudClaim command)
        {
            var (userId, appId, projectId) = command;
            var claim = await _repository.GetAsync(userId, appId, projectId);
            if (claim is null)
            {
                throw new ClaimNotFoundException();
            }
            await _repository.RemoveAsync(claim);
        }
    }
}