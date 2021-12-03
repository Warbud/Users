using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.Commands.WarbudApp;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.WarbudApp;

namespace Warbud.Users.Controllers
{
    public class AppController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IValidator<AddWarbudApp> _appValidator;
        private readonly ICommandDispatcher _commandDispatcher;

        public AppController(IQueryDispatcher queryDispatcher,
            IValidator<AddWarbudApp> appValidator,
            ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _appValidator = appValidator;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<ActionResult<WarbudAppDto>> GetApp([FromBody] GetWarbudApp query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpGet]
        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<ActionResult<IEnumerable<WarbudAppDto>>> GetApps()
        {
            var result = await _queryDispatcher.QueryAsync(new GetWarbudApps());
            return OkOrNotFound(result);
        }
        
        [HttpPost]
        [Authorize(Roles = new []{Role.Name.Admin})]
        public async Task<ActionResult> AddAppAsync([FromBody] AddWarbudApp command)
        {
            await _appValidator.ValidateAndThrowAsync(command);
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
        
        [HttpPost]
        [Authorize(Roles = new []{ Role.Name.Admin})]
        public async Task<ActionResult> UpdateAppAsync([FromBody] UpdateWarbudApp command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
        
        [HttpPost]
        [Authorize(Roles = new []{ Role.Name.Admin})]
        public async Task<ActionResult> RemoveAppAsync([FromBody] RemoveWarbudApp command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
    }
}