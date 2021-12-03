using System.Threading.Tasks;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.Exceptions;
using Warbud.Users.Domain.Repositories;

namespace Warbud.Users.Application.Commands.WarbudApp.Handlers
{
    public class RemoveWarbudAppHandler: ICommandHandler<RemoveWarbudApp>
    {
        private readonly IWarbudAppRepository _warbudAppRepository;

        public RemoveWarbudAppHandler(IWarbudAppRepository warbudAppRepository)
        {
            _warbudAppRepository = warbudAppRepository;
        }

        public async Task HandleAsync(RemoveWarbudApp command)
        {
            var warbudApp = await _warbudAppRepository.GetAsync(command.Id);
            if (warbudApp is null)
            {
                throw new AppNotFoundException();
            }
            await _warbudAppRepository.RemoveAsync(warbudApp);
        }
    }
}