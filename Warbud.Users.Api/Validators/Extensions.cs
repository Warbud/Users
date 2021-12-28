using System.Linq;

namespace Warbud.Users.Api.Validators
{
    public static class Extensions
    {
        public static bool HasSpecialChars(this string input)
        {
            return input.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }
}