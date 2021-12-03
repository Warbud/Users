using System.Threading.Tasks;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.Exceptions;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Application.Commands.WarbudApp.Handlers
{
    public class UpdateWarbudAppHandler : ICommandHandler<UpdateWarbudApp>
    {
        private readonly IWarbudAppRepository _appRepository;

        public UpdateWarbudAppHandler(IWarbudAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        public async Task HandleAsync(UpdateWarbudApp command)
        {
            var warbudApp = await _appRepository.GetAsync(command.Id);
            if (warbudApp is null)
            {
                throw new AppNotFoundException();
            }
            warbudApp.UpdateEntity(command);
            await _appRepository.UpdateAsync(warbudApp);
        }
    }
}