using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Users.Application.Exceptions
{
    public class ModuleAlreadyInUseException : WarbudException
    {
        public string AppName { get; }
        public string ModuleName { get; }

        public ModuleAlreadyInUseException(string appName, string moduleName) : base(
            $"Module '{moduleName}' in app '{appName}' already exists.")

        {
            AppName = appName;
            ModuleName = moduleName;
        }
        
    }
}