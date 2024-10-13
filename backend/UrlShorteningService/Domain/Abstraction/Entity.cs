namespace ShorteningService.Domain.Abstraction
{
    public abstract class Entity<T>
    {

        public T Id { get; protected set; } = default!;
    }
}
