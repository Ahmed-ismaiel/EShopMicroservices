using MediatR;

namespace Ordering.Domain.Abstractions
{

    // IDomainEvent interface represents a domain event in the system.
    // It extends the INotification interface from MediatR, allowing it to be used with the MediatR library for event handling.
    // The interface defines properties for the event ID, occurrence time, and event type.
    public interface IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.Now;

        public string EventType => GetType().AssemblyQualifiedName;                                                  
    }
}
