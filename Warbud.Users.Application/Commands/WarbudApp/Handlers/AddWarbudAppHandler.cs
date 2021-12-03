using System.Threading.Tasks;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.Exceptions;
using Warbud.Users.Application.Services;
using Warbud.Users.Domain.Factories.WarbudApp;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Application.Commands.WarbudApp.Handlers
{
    public class AddWarbudAppHandler : ICommandHandler<AddWarbudApp>
    {
        private readonly IWarbudAppRepository _repository;
        private readonly IWarbudAppReadService _readService;
        private readonly IWarbudAppFactory _factory;
        public AddWarbudAppHandler(IWarbudAppRepository repository,
            IWarbudAppReadService readService, IWarbudAppFactory factory)
        {
            _repository = repository;
            _readService = readService;
            _factory = factory;
        }
        
        public async Task HandleAsync(AddWarbudApp command)
        {
            var (appName, moduleName) = command;
            //TODO: Move to validators
            if (await _readService.ExistsAsync(appName, moduleName))
            {
                throw new ModuleAlreadyInUseException(appName, moduleName);
            }

            var user = _factory.Create(appName, moduleName );
            await _repository.AddAsync(user);
        }
    }
}