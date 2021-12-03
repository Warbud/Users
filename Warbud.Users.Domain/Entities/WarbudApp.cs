using Warbud.Shared.Abstraction;
using Warbud.Shared.Interfaces;

namespace Warbud.Users.Domain.Entities
{
    public class WarbudApp : AuditableEntity, IEntity
    {
        private WarbudApp()
        {
            
        }

        public WarbudApp(string appName, string moduleName)
        {
            AppName = appName;
            ModuleName = moduleName;
        }

        public int Id { get; set; }
        public string AppName { get; set; }
        public string ModuleName { get; set; }
    }
}