namespace Warbud.Users.Application.Commands
{
    public interface IInput<T>
    {
        public T Id { get; init; }
    }
}