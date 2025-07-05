

using Ordering.Domain.Events;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        // The Order class represents an order in the domain model.
        // It extends the Aggregate<Guid> class, indicating

        private readonly List<OrderItem> _orderItems = new();
        // that it is an aggregate root with a specific identifier type of Guid.
        public IReadOnlyList<OrderItem> Items => _orderItems.AsReadOnly();

        public CustomerId CustomerId { get; private set; }

        public OrderName OrderName { get; private set; }

        public Address ShippingAddress { get; private set; }

        public Address BillingAddress { get; private set; }

        public Payment Payment { get; private set; }

        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public decimal TotalPrice
        {

            get
            {
                return _orderItems.Sum(item => item.Price * item.Quantity);
            }

            private set
            {
                // TotalPrice is calculated based on the items in the order.
                // It should not be set directly, but rather calculated from the items.
                throw new InvalidOperationException("TotalPrice cannot be set directly. It is calculated from the order items.");
            }

        }

        public static Order Create(
            OrderId id,
            CustomerId customerId,
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment)
        {
            // Factory method to create a new order instance.
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (customerId == null) throw new ArgumentNullException(nameof(customerId));
            if (orderName == null) throw new ArgumentNullException(nameof(orderName));
            if (shippingAddress == null) throw new ArgumentNullException(nameof(shippingAddress));
            if (billingAddress == null) throw new ArgumentNullException(nameof(billingAddress));
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment
            };
            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }

        public void Update(
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment)
        {
            // Method to update the order details.
            if (orderName == null) throw new ArgumentNullException(nameof(orderName));
            if (shippingAddress == null) throw new ArgumentNullException(nameof(shippingAddress));
            if (billingAddress == null) throw new ArgumentNullException(nameof(billingAddress));
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            AddDomainEvent(new OrderUpdatedEvent(this));
        }


        public void Add(ProductId productId, int quantity, decimal price)
        { 
        
            // Method to add an item to the order.
            if (productId == null) throw new ArgumentNullException(nameof(productId));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
            if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");
            var orderItem = new OrderItem(Id, productId, quantity, price);
            _orderItems.Add(orderItem);

        }

        public void Remove(OrderItemId orderItemId)
        {
            // Method to remove an item from the order.
            if (orderItemId == null) throw new ArgumentNullException(nameof(orderItemId));
            var orderItem = _orderItems.FirstOrDefault(item => item.Id.Equals(orderItemId));
            if (orderItem == null)
            {
                throw new InvalidOperationException($"Order item with ID {orderItemId} not found.");
            }
            _orderItems.Remove(orderItem);
        }

    }
}
