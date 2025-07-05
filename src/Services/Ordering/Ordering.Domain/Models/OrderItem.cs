using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models
{
    public class OrderItem  : Entity<OrderItemId>
    {

        public OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
        {
            Id = OrderItemId.of(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        // The OrderItem class represents an item in an order in the domain model.

        public OrderId OrderId { get; private set; }

        public ProductId ProductId { get; private set; }

        public int Quantity { get; private set; }

        public decimal Price { get; private set; }



    }
}