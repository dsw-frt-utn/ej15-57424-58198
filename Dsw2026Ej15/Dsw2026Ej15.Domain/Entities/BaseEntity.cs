namespace Dsw2026Ej15.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }

    protected BaseEntity()
    {
    }

    protected BaseEntity(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
    }
}