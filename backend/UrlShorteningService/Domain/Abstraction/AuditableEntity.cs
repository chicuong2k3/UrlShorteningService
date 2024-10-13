

namespace ShorteningService.Domain.Abstraction;

public abstract class AuditableEntity<T> : Entity<T>
{
    public DateTime CreatedAt { get; protected set; }
    protected AuditableEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
