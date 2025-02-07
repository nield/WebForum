using System.ComponentModel.DataAnnotations.Schema;

namespace WebForum.Domain.Common;

public abstract class BaseEntity
{
    private readonly List<BaseEvent> _domainEvents = new();

    public long Id { get; set; }

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
