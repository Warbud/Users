using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.Commands.WarbudClaim;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.WarbudClaim;

namespace Warbud.Users.Api.Controllers
{
    public class ClaimController : BaseController
    {
        private readonly IValidator<AddWarbudClaim> _claimValidator;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ClaimController(IValidator<AddWarbudClaim> claimValidator,
            ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _claimValidator = claimValidator;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        [Authorize(Roles = new []{ Role.Name.Admin})]
        public async Task<ActionResult> AddClaimAsync([FromBody] AddWarbudClaim command)
        {
            await _claimValidator.ValidateAsync(command);
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
        
        [HttpPost]
        [Authorize(Roles = new []{Role.Name.Admin})]
        public async Task<ActionResult> UpdateClaimAsync([FromBody] UpdateWarbudClaim command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
        
        [HttpPost]
        [Authorize(Roles = new []{ Role.Name.Admin})]
        public async Task<ActionResult> RemoveClaimAsync([FromBody] RemoveWarbudClaim command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
        
        [HttpGet]
        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<ActionResult<WarbudClaimDto>> GetClaimById([FromBody] GetWarbudClaim query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpGet]
        [Authorize(Policy = Policy.Name.AdminOrOwner)]
        public async Task<ActionResult<IEnumerable<WarbudClaimDto>>> GetClaimsByUserId([FromBody] GetWarbudClaimsByUserId query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpGet]
        [Authorize(Roles = new[] {Role.Name.Admin})]
        public  ActionResult<IEnumerable<string>> GetClaimValues()
        {
            var result =  Claim.Value.GetValueList();
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}