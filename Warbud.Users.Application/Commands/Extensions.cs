using System.Linq;
using Warbud.Shared.Abstraction.Interfaces;
using Warbud.Shared.Interfaces;

namespace Warbud.Users.Application.Commands
{
    internal static class Extensions
    {
        public static void UpdateEntity<TEntity, TInput>(this TEntity entity, TInput input) where TEntity : IEntity
        {
            foreach (var prop in input.GetType().GetProperties().Where(x => x.GetValue(input) is not null))
            {
                var propInfo = entity.GetType().GetProperty(prop.Name);
                
                if (propInfo.GetValue(entity).GetType().GetInterface(typeof(IValueType<>).Name) is null)
                {
                    propInfo.SetValue(entity, prop.GetValue(input));
                    continue;
                }
                propInfo.PropertyType.GetProperty("Value").SetValue(propInfo.GetValue(entity), prop.GetValue(input));
                
            }
        }
    }
}