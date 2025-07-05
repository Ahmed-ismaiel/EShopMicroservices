using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record ProductId
    {

        public Guid Value { get; init; } = default!;
        private ProductId(Guid value) => Value = value;
        public static ProductId of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value == Guid.Empty)
            {
                throw new ArgumentException("ProductId cannot be an empty GUID.", nameof(value));
            }
            return new ProductId(value);
        }


    }
}
