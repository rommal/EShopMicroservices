using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models.ValueObjects
{
    public record OrderItemId
    {
        private OrderItemId(Guid value) => Value = value;

        public Guid Value { get; }

        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value == Guid.Empty)
                throw new DomainException("OrderItemId cannot be empty.");

            return new OrderItemId(value);
        }
    }
}
