using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models
{
    public class Customer : Entity<CustomerId>
    {
        // The Cutomer class represents a customer in the domain model.
        // It extends the Entity<Guid> class, indicating that it is an entity with a specific identifier type of Guid.
        // The class currently does not have any properties or methods defined.
        // You can add properties and methods related to customer information as needed.

        public string Name { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public static Customer Create(CustomerId id, string name, string email)
        {
            // Factory method to create a new customer instance.
            // It initializes the Name and Email properties and returns a new Cutomer instance.
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }
            return new Customer
            {
                Id = id,
                Name = name,
                Email = email
            };
        }
    }
}
