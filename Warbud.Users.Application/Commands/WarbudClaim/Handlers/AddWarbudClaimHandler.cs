using System.Threading.Tasks;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.Exceptions;
using Warbud.Users.Application.Services;
using Warbud.Users.Domain.Factories.WarbudClaim;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Application.Commands.WarbudClaim.Handlers
{
    public class AddWarbudClaimHandler : ICommandHandler<AddWarbudClaim>
    {
        private readonly IWarbudClaimRepository _repository;
        private readonly IWarbudClaimReadService _claimReadService;
        private readonly IWarbudClaimFactory _factory;
        public AddWarbudClaimHandler(IWarbudClaimRepository repository,
            IWarbudClaimReadService claimReadService, IWarbudClaimFactory factory)
        {
            _repository = repository;
            _claimReadService = claimReadService;
            _factory = factory;
        }
        
        public async Task HandleAsync(AddWarbudClaim command)
        {
            var (userId, appId, projectId, name) = command;
            
            //TODO: Move to validators
            if (await _claimReadService.ExistsByKeyAsync(userId, appId, projectId))
            {
                throw new ClaimAlreadyInUseException(userId, appId, projectId);
            }

            var claim = _factory.Create(userId, appId, projectId, name );
            await _repository.AddAsync(claim);
        }
    }
}