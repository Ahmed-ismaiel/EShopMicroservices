using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record OrderId
    {
        public Guid Value { get; set; }


        private OrderId(Guid value) => Value = value;

        public static OrderId of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value == Guid.Empty)
            {
                throw new DomainException("OrderId cannot be an empty GUID.", nameof(value));
            }
            return new OrderId(value);
        }

    }
}
