using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLenght = 5;
        public string Value { get; init; } = default!;
        private OrderName(string value) => Value = value;
       
        public static OrderName of(string value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value.Length < DefaultLenght)
            {
                throw new ArgumentException($"Order name must be at least {DefaultLenght} characters long.", nameof(value));
            }
            return new OrderName(value);
        }
    }
}
