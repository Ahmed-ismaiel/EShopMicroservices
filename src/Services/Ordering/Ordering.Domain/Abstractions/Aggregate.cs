
namespace Ordering.Domain.Abstractions
{
    public class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    {

        // The Aggregate class represents an aggregate root in the domain model.
        // It extends the Entity<TId> class, indicating that it is an entity with a specific identifier type TId.
        // The class implements the IAggregate<TId> interface, which defines a read-only collection of domain events associated with the aggregate.
        // It also provides a method to clear the domain events associated with the aggregate.

        private readonly List<IDomainEvent>  _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            // The AddDomainEvent method adds a domain event to the aggregate's collection of domain events.
            if (domainEvent == null)
            {
                throw new ArgumentNullException(nameof(domainEvent), "Domain event cannot be null.");
            }
            _domainEvents.Add(domainEvent);
        }

        public IDomainEvent[] ClearDomainEvents()
        {
            // The ClearDomainEvents method clears the aggregate's collection of domain events and returns an array of the cleared events.

            IDomainEvent[] dequeuedEvents = _domainEvents.ToArray();
            _domainEvents.Clear();
            return dequeuedEvents;
        }
    }
}
