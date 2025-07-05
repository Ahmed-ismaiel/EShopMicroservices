namespace Ordering.Domain.Abstractions
{
    
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {
        // The interface extends the IAggregate interface and the IEntity<T> interface,
        // indicating that it is an aggregate root with a specific identifier type T.
    }





    // IAggregate interface represents an aggregate root in the domain model.
    // It extends the IEntity interface, indicating that it is an entity with an identifier.
    // // The interface defines a read-only collection of domain events associated with the aggregate.
    public interface IAggregate : IEntity
    {
        
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        // Method to clear the domain events associated with the aggregate.
        // This method returns an array of IDomainEvent objects that were cleared.
        IDomainEvent[] ClearDomainEvents();

    }
}
