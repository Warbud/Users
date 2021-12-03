using System.Threading.Tasks;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.Exceptions;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Application.Commands.WarbudClaim.Handlers
{
    public class UpdateWarbudClaimHandler : ICommandHandler<UpdateWarbudClaim>
    {
        private readonly IWarbudClaimRepository _repository;

        public UpdateWarbudClaimHandler(IWarbudClaimRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(UpdateWarbudClaim command)
        {
            var (userId, appId, projectId, value) = command;
            var claim = await _repository.GetAsync(userId, appId, projectId);
            if (claim is null)
            {
                throw new ClaimNotFoundException();
            }
            claim.Value = value;
            await _repository.UpdateAsync(claim);
        }
    }
}