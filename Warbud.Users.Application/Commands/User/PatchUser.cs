using System;
using Microsoft.AspNetCore.JsonPatch;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.DTO;

namespace Warbud.Users.Application.Commands.User
{
    public record PatchUser(Guid Id, JsonPatchDocument<PatchUserDto> PatchDoc) : ICommand;
}