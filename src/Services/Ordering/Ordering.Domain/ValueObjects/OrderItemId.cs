using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record OrderItemId
    {
        public Guid Value { get; set; }
        private OrderItemId(Guid value) => Value = value;
        public static OrderItemId of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value == Guid.Empty)
            {
                throw new ArgumentException("OrderItemId cannot be an empty GUID.", nameof(value));
            }
            return new OrderItemId(value);
        }
    }
}
