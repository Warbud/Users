using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.Commands.User;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.User;

namespace Warbud.Users.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        
        public UserController(IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }
        
        [HttpPost]
        public async Task<ActionResult> AddUserAsync([FromBody] AddUser command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        [Authorize(Policy = Policy.Name.AdminOrOwner)]
        public async Task<ActionResult> PatchUserAsync([FromRoute] Guid id,
            [FromBody] JsonPatchDocument<PatchUserDto> patchDoc)
        {
            await _commandDispatcher.DispatchAsync(new PatchUser(id, patchDoc));
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = Role.Name.Admin)]
        public async Task<ActionResult> UpdateUserRoleAsync([FromBody] UpdateUserRole command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = Role.Name.Admin)]
        public async Task<ActionResult> RemoveUserAsync([FromRoute] RemoveUser command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> MeAsync()
        {
            var result = await _queryDispatcher.QueryAsync(new QueryMe());
            return OkOrNotFound(result);
        }

        [HttpGet]
        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync()
        {
            var result = await _queryDispatcher.QueryAsync(new GetUsers());
            return OkOrNotFound(result);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<ActionResult<UserDto>> GetUserAsync([FromRoute] GetUserById query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> LoginAsync([FromBody] LoginUser query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(new {token = result});
        }
        
        [HttpPut]
        [Authorize(Policy = Policy.Name.AdminOrOwner)]
        public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePassword command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }
    }
}