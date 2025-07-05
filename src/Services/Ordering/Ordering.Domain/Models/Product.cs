using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        // The Product class represents a product in the domain model.
        // It extends the Entity<Guid> class, indicating that it is an entity with a specific identifier type of Guid.
        // The class currently does not have any properties or methods defined.
        // You can add properties and methods related to product information as needed.
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; }

        public static Product Create(ProductId id, string name, decimal price)
        {
            // Factory method to create a new product instance.
            // It initializes the Name and Price properties and returns a new Product instance.
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");
            }
            return new Product
            {
                Id = id,
                Name = name,
                Price = price
            };
        }
    }
   
}
