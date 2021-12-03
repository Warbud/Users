using Warbud.Users.Application.DTO;
using Warbud.Users.Infrastructure.EF.Models;

namespace Warbud.Users.Infrastructure.EF.Queries
{
    internal static class Extensions
    {
        public static UserDto AsDto(this UserReadModel readModel)
            => new()
            {
                Id = readModel.Id,
                FirstName = readModel.FirstName,
                LastName = readModel.LastName,
                Email = readModel.Email,
                Role = readModel.Role
            };
        
        public static WarbudAppDto AsDto(this WarbudAppReadModel readModel)
            => new()
            {
                Id = readModel.Id,
                AppName = readModel.AppName,
                ModuleName = readModel.ModuleName
            };
        
        public static WarbudClaimDto AsDto(this WarbudClaimReadModel readModel)
            => new()
            {
                UserId = readModel.UserId,
                AppId = readModel.AppId,
                ProjectId = readModel.ProjectId,
                Value = readModel.Value
            };
    }
}